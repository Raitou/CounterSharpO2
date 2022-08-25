using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Net;
using System.Threading.Channels;
using TCPServer.Handler;
using TCPServer.Packet.Core;
using TCPServer.Client;
using System.Text;
using DotNetty.Buffers;

namespace TCPServer.Startup
{
    public static class Startup
    {

        static bool bRunning = true;

        public static void Start()
        {

            MultithreadEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            MultithreadEventLoopGroup workerGroup = new MultithreadEventLoopGroup();

            try
            {
                // https://github.com/Azure/azure-iot-protocol-gateway/blob/master/docs/DeveloperGuide.md
                ServerBootstrap serverBootstrap = new ServerBootstrap();
                serverBootstrap
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(
                            new PacketDecoder(),
                            new ChannelHandler(),
                            new PacketEncoder()
                            );

                        TCPClient client = new TCPClient(channel);

                        IByteBuffer byteBuffer = Unpooled.Buffer(1024);
                        byteBuffer.WriteString("~SERVERCONNECTED\n", Encoding.UTF8);

                        channel.WriteAndFlushAsync(byteBuffer).GetAwaiter().GetResult();
                        Console.WriteLine("Client {0} is connected", client.Channel.RemoteAddress.ToString());
                    }))
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true);

                IChannel bootstrapChannel = serverBootstrap.BindAsync(
                    IPAddress.Parse("10.0.0.2")
                    , 30001
                    ).GetAwaiter()
                    .GetResult();

                Console.WriteLine("Started TCP Server on {0}", bootstrapChannel.LocalAddress.ToString());

                Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
                {
                    e.Cancel = true;
                    Stop();
                };

                while (bRunning) ;

                bootstrapChannel.CloseAsync().GetAwaiter();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); // TODO: make a logger
            }
            finally
            {
                Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
                Console.WriteLine("TCP Server exited gracefully"); // TODO: make a logger
            }
        }

        public static void Stop()
        {
            bRunning = false;
        }
    }
}
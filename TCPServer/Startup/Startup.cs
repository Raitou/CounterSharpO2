using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Net;
using TCPServer.Packet;

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
                            new PacketEncoder()
                            );
                    }))
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true);

                IPAddress ipAdd = IPAddress.Parse("9.0.0.2");
                IChannel bootstrapChannel = serverBootstrap.BindAsync(ipAdd, 30001).GetAwaiter().GetResult();

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
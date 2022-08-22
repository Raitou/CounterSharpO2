using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace Server
{
    public static class TCPServer {

        static bool bRunning = true;

        public static async Task Start()
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
                    }));


                IChannel bootstrapChannel = await serverBootstrap.BindAsync(30001); // TODO: make this in a config

                Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e) {
                    e.Cancel = true;
                    Stop();
                };

                while (bRunning) ;
                
                await bootstrapChannel.CloseAsync();

            } catch (Exception ex)
            {
               Console.WriteLine(ex.ToString()); // TODO: make a logger
            } finally
            {
                Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
                Console.WriteLine("exited gracefully"); // TODO: make a logger
            }
        }

        public static void Stop()
        {
            bRunning = false;
        }

        
    }
}
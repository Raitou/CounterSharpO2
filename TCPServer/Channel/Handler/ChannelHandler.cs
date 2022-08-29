using Common.Packet.Enum;
using Common.Utilities;
using DotNetty.Transport.Channels;
using TCPServer.Client;
using TCPServer.Packet.Core;
using TCPServer.Channel.Helper;

namespace TCPServer.Channel.Handler
{
    internal class ChannelHandler : SimpleChannelInboundHandler<PacketData>
    {
        private ChannelHelper _channelHelper;
        public ChannelHandler()
        {
            // Since every connection creates a new set of its own pipeline then we shouldn't
            // really practice static here especially on a helper class so we make it an
            // object member instead
            _channelHelper = new ChannelHelper();

        }

        // Initial Connection
        public override void ChannelActive(IChannelHandlerContext context)
        {
           _channelHelper.OnClientConnect(context);
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, PacketData msg)
        {
            try
            {
                switch (msg.PacketID)
                {
                    case PacketID.VersionInfo:
                        {
                            _channelHelper.OnVersionInfo(ctx);
                        }
                        break;

                    //case PacketID.Login:
                    //    {
                    //        ChannelHelper.OnLogin(ctx, msg);
                    //    }
                    //    break;

                    default:
                        {
                            //if packet not found
                            Console.WriteLine(msg.PacketID + ": " + ByteUtil.ByteArrToString(msg.RawData));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}

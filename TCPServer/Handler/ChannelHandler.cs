using Common.Client;
using Common.Packet;
using Common.Packet.Enum;
using Common.Utilities;
using DotNetty.Transport.Channels;
using TCPServer.Packet.Core;
using TCPServer.Packet.Info;

namespace TCPServer.Handler
{
    internal class ChannelHandler : SimpleChannelInboundHandler<PacketData>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, PacketData msg)
        {
            IPacket? packet = null;
           
            switch (msg.PacketID)
            {
                case PacketID.VersionInfo: 
                    {
                        packet = new VersionInfo(versionHash: "6246015df9a7d1f7311f888e7e861f18"); // TODO: Should move to config
                    } break;

                default:
                    {
                        //if packet not found
                        Console.WriteLine(msg.PacketID + ": " + ByteUtil.ByteArrToString(msg.RawData));
                    } break;
            }

            if(packet != null)
                ctx.Channel.WriteAndFlushAsync(packet.BuildPacket());
        }
    }
}

using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Packet;

namespace TCPServer.Handler
{
    internal class ChannelHandler : SimpleChannelInboundHandler<PacketIn>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, PacketIn msg)
        {
            // do nothing
        }
    }
}

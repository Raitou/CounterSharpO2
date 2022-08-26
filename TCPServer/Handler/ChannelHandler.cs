using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Packet.Core;

namespace TCPServer.Handler
{
    internal class ChannelHandler : SimpleChannelInboundHandler<IPacketInterface>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, IPacketInterface msg)
        {
            // do nothing
        }
    }
}

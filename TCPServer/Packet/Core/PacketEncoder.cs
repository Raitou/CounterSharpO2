using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Packet.Core
{
    internal class PacketEncoder : MessageToByteEncoder<IPacketInterface>
    {
        protected override void Encode(IChannelHandlerContext context, IPacketInterface message, IByteBuffer output)
        {
            throw new NotImplementedException();
        }
    }
}

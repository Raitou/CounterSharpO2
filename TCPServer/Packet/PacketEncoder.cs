using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Packet
{
    internal class PacketEncoder : MessageToByteEncoder<PacketOut>
    {
        protected override void Encode(IChannelHandlerContext context, PacketOut message, IByteBuffer output)
        {
            throw new NotImplementedException();
        }
    }
}

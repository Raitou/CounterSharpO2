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
    internal class PacketEncoder : MessageToByteEncoder<PacketOut>
    {
        protected override void Encode(IChannelHandlerContext context, PacketOut message, IByteBuffer output)
        {
            Console.WriteLine("aaaaaaaaa");
            throw new NotImplementedException();
        }
    }
}

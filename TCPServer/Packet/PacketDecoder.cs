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
    internal class PacketDecoder : ByteToMessageDecoder
    {

        protected override void Decode(IChannelHandlerContext _context, IByteBuffer _input, List<object> _output)
        {
            if (_input == null)
            {
                return;
            }
            if (_input.ReadableBytes <= 0)
            {
                return;
            }

            Console.WriteLine(_input.ReadString(_input.ReadableBytes, Encoding.UTF8));
        }
    }
}

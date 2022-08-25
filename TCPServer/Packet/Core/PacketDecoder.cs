using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Utilities;

namespace TCPServer.Packet.Core
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

            //if packet not found
            Console.WriteLine("" + Common.ByteBufToString(_input, _input.ReadableBytes));
        }
    }
}

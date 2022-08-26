using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Packet.Core;

namespace TCPServer.Packet.Info
{
    internal class ServerConnected : IPacketInterface
    {
        public String StrMessage { get; set; }
        private IByteBuffer _buffer = Unpooled.Buffer(1024);
        public ServerConnected(string strMessage)
        {
            StrMessage = strMessage;
        }

        public IPacketInterface BuildPacket()
        {
            _buffer.Clear();
            _buffer.WriteString("~SERVERCONNECTED\n", Encoding.UTF8);
            return this;
        }

        public IByteBuffer? GetPacket()
        {
            if (_buffer.ReadableBytes < 0)
                return null;
            return _buffer;
        }
    }
}

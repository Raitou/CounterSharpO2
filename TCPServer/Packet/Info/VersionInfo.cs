using Common.Packet;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Packet.Info
{
    internal class VersionInfo : IPacket
    {
        public string Version { get; set; }

        private IByteBuffer _buffer = Unpooled.Buffer(1024);
        public VersionInfo(string versionHash)
        {
            Version = versionHash;
        }

        public IPacket BuildPacket()
        {
            _buffer.WriteByte(0); //packetID
            _buffer.WriteByte(0); //isbadhash
            _buffer.WriteString(Version, Encoding.ASCII); //versionhashh
            return this;
        }

        public IByteBuffer? GetPacket()
        {
            if (_buffer.ReadableBytes <= 0)
                return null;
            return _buffer;
        }
    }
}

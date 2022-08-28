using Common.Packet;
using Common.Packet.Enum;
using DotNetty.Buffers;
using System.Text;

namespace TCPServer.Packet.Info
{
    internal class VersionInfo : IPacket
    {
        public string Version { get; set; }
        private bool _isBadhash = false;
        public VersionInfo(string versionHash, bool isBadHash = false)
        {
            Version = versionHash;
            _isBadhash = isBadHash;
        }

        private IByteBuffer _buffer = Unpooled.Buffer(1024);
        public IPacket BuildPacket()
        {
            _buffer.WriteByte((int)PacketID.VersionInfo);
            _buffer.WriteBoolean(_isBadhash);
            _buffer.WriteString(Version, Encoding.ASCII);
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

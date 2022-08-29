using Common.Packet;
using Common.Packet.Enum;
using DotNetty.Buffers;
using System.Text;

namespace TCPServer.Packet
{
    internal class ClientConnect : IPacket
    {
        public string StrMessage { get; set; }
        public IByteBuffer ByteBuffer { get; }
        public PacketID PacketID { get; }

        public ClientConnect(string strMessage)
        {
            StrMessage = strMessage;
            ByteBuffer = Unpooled.Buffer(1024);
            PacketID = PacketID.ClientConnect;
        }

        public IPacket BuildPacket()
        {
            ByteBuffer.Clear();
            ByteBuffer.WriteString(StrMessage, Encoding.UTF8);
            return this;
        }

        public IPacket? GetPacket()
        {
            if (ByteBuffer.ReadableBytes <= 0)
                return null;
            return this;
        }
    }
}

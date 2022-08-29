using Common.Packet.Enum;
using DotNetty.Buffers;

namespace Common.Packet
{
    public interface IPacket
    {
        IByteBuffer ByteBuffer { get; }
        PacketID PacketID { get; }
        IPacket BuildPacket();
        IPacket? GetPacket();
       
    }
}

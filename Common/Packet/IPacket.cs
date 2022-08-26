using DotNetty.Buffers;

namespace Common.Packet
{
    public interface IPacket
    {
        IPacket BuildPacket();
        IByteBuffer? GetPacket();
    }
}

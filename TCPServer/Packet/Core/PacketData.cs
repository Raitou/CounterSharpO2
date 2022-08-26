

namespace TCPServer.Packet.Core
{
    internal class PacketData
    {
        public byte[] RawData { get; }
        public PacketData(byte[] rawData)
        {
            RawData = rawData;
            PacketID = RawData[0];
            RawData = RawData.Skip(1).ToArray();
        }

        public short PacketID { get; set; }
    }
}

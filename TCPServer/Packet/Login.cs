using Common.Packet;
using Common.Packet.Enum;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Packet.Core;

namespace TCPServer.Packet
{
    internal class Login : IPacketParser
    {
        public PacketData PacketData { get; }

        public IByteBuffer ByteBuffer => throw new NotImplementedException();

        public PacketID PacketID => throw new NotImplementedException();

        public Login(PacketData packetData)
        {
            PacketData = packetData;
        }

        public IPacketParser ParsePacket()
        {
            throw new NotImplementedException();
        }

        public IPacket BuildPacket()
        {
            throw new NotImplementedException();
        }

        public IPacket? GetPacket()
        {
            throw new NotImplementedException();
        }
    }
}

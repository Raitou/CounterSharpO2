using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packet.Enum
{
    public enum PacketID
    {
        VersionInfo = 0,
        UnknownPacket01,
        Login,

        ClientConnect = 255
    }

    public enum PacketSignature
    {
        TCPSignature = 85
    }
}

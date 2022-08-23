using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Packet
{
    public abstract class PacketAbstract : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

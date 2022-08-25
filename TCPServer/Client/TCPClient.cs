using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Client
{
    internal class TCPClient
    {

        public IChannel Channel { get; set; }

        public TCPClient(IChannel _ch)
        {
            Channel = _ch;
        }
    }
}

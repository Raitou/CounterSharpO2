using DotNetty.Transport.Channels;
using TCPServer.Packet.Core;

namespace TCPServer.Client
{
    internal class TCPClient
    {

        public IChannel Channel { get; set; }

        public TCPClient(IChannel _ch)
        {
            Channel = _ch;
        }

        public void Send(IPacketInterface _packet)
        {
            Channel.WriteAndFlushAsync(
                _packet
                .BuildPacket()
                .GetPacket())
            .GetAwaiter()
            .GetResult();
        }
    }
}

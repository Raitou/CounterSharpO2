using Common.Client;
using Common.Packet;
using DotNetty.Transport.Channels;

namespace TCPServer.Client
{
    internal class TCPClient : IClient
    {
        public IChannel Channel { get; set; }

        public byte Sequence { get; set; }

        public TCPClient(IChannel _ch)
        {
            Channel = _ch;
            Sequence = 0x0;
        }

        public void Send(IPacket _packet)
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

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
            Sequence = 0;
        }

        /**
         * <summary>
         * This bypasses the PacketEncoder class and proceeds to sending the packet directly.
         * Used only for sending initial packet or handshake between server and client.
         * If you want the packet to go through the PacketEncoder class use ChannelHandlerContext
         * and use it to send packets.
         * </summary>
         */
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

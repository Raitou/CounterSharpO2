using Common.Packet;
using DotNetty.Transport.Channels;

namespace Common.Client
{
    public interface IClient
    {
        public IChannel Channel { get; set; }
        void Send(IPacket _packet);
    }
}

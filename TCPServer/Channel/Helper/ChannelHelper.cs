using Common.Client;
using Common.Packet;
using DotNetty.Transport.Channels;
using TCPServer.Packet;
using TCPServer.Packet.Core;

namespace TCPServer.Channel.Helper
{
    internal class ChannelHelper
    {
        public void OnClientConnect(IChannelHandlerContext ctx)
        {
            ClientConnect serverConnected = new ClientConnect(strMessage: "~SERVERCONNECTED\n");
            IPacket? byteBuffer = serverConnected.BuildPacket();
            if (byteBuffer == null)
                throw new Exception("ServerConnected packet is empty!");
            ctx.WriteAndFlushAsync(byteBuffer);

            Console.WriteLine("Client {0} is connected", ctx.Channel.RemoteAddress);
        }

        public void OnVersionInfo(IChannelHandlerContext ctx)
        {
            //client ignores this so there's really no point of putting a hash here but it needs it so yup
            VersionInfo versionInfo = new VersionInfo(versionHash: "6246015df9a7d1f7311f888e7e861f18");

            IPacket? byteBuffer = versionInfo.BuildPacket();
            if (byteBuffer == null)
                throw new Exception("VersionInfo packet is empty!");
            ctx.WriteAndFlushAsync(byteBuffer);

            Console.WriteLine("VersionInfo send to {0}", ctx.Channel.RemoteAddress);
        }

        public void OnLogin(IChannelHandlerContext ctx, PacketData msg)
        {
            throw new NotImplementedException();
        }
    }
}

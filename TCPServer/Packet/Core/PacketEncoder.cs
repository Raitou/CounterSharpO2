using Common.Packet;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using TCPServer.Client;

namespace TCPServer.Packet.Core
{
    internal class PacketEncoder : MessageToByteEncoder<IPacket>
    {
        private TCPClient _client;
        public PacketEncoder(TCPClient client)
        {
            _client = client;
        }

        private readonly object _lock = new object();
        protected override void Encode(IChannelHandlerContext context, IPacket message, IByteBuffer output)
        {
            lock (_lock)
            {
                try
                {
                    if (message.GetPacket() == null)
                        throw new Exception();

                    output.WriteByte(createHeader());
                    output.WriteByte(getSequence());
                    output.WriteShortLE(message.GetPacket().ReadableBytes);
                    output.WriteBytes(message.GetPacket());
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (message.GetPacket() != null)
                        message.GetPacket().Clear();
                }
            }
        }

        private byte createHeader()
        {
            return 0x55; // PacketSignature
        }

        private byte getSequence()
        {
            return _client.Sequence++;
        }
    }
}

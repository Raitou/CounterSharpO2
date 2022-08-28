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
#pragma warning disable CS8602 // Null check already defined above
                    output.WriteShortLE(message.GetPacket().ReadableBytes);
#pragma warning restore CS8602
                    output.WriteBytes(message.GetPacket());
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (message.GetPacket() != null)
#pragma warning disable CS8602 // Null check already defined above
                        message.GetPacket().Clear();
#pragma warning restore CS8602
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

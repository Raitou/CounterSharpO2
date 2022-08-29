using Common.Packet;
using Common.Packet.Enum;
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

                    if(message.PacketID != PacketID.ClientConnect)
                    {
                        output.WriteByte(createHeader());
                        output.WriteByte(getSequence());
                        output.WriteShortLE(message.ByteBuffer.ReadableBytes);
                    }
                    output.WriteBytes(message.ByteBuffer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (message.GetPacket() != null)
                        message.ByteBuffer.Clear();
                }
            }
        }

        private bool isInitialPacket()
        {

            return true;
        }

        private byte createHeader()
        {
            return (byte)PacketSignature.TCPSignature;
        }

        private byte getSequence()
        {
            return _client.Sequence++;
        }
    }
}

using Common.Utilities;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using TCPServer.Client;

namespace TCPServer.Packet.Core
{
    internal class PacketDecoder : ByteToMessageDecoder
    {
        private TCPClient _client;
        public PacketDecoder(TCPClient client)
        {
            _client = client;
        }

        private readonly object _lock = new object();
        protected override void Decode(IChannelHandlerContext _context, IByteBuffer _input, List<object> _output)
        {
            lock(_lock)
            {
                try
                {
                    if (_input == null)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    if (_input.ReadableBytes <= 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    if (validateHeader(_input) != true)
                    {
                        throw new IndexOutOfRangeException(); // todo: disconnect client for now just ignore
                    }
                    setSequence(_input.ReadByte());
                    _output.Add(new PacketData(parseRawData(_input)));
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    _input.Clear();
                }
            }
           
        }

        private byte[] parseRawData(IByteBuffer _input)
        {
            int packetLen = _input.ReadShortLE();
            byte[] packetData = new byte[packetLen];
            _input.ReadBytes(packetData);
            return packetData;
        }

        private void setSequence(byte _seq)
        {
            _client.Sequence = _seq;
        }

        private bool validateHeader(IByteBuffer _input)
        {
            return _input.ReadByte() == 0x55; //0x55 = PacketSignature
        }
    }
}

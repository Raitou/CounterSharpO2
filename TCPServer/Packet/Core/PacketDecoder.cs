using Common.Packet.Enum;
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
                    byte header = _input.ReadByte();
                    if (validateHeader(header) != true)
                    {
                        throw new Exception(String.Format("Invalid Header. Header expect {0} but received {1}", 
                            PacketSignature.TCPSignature,
                            header));
                    }
                    byte seq = _input.ReadByte();
                    if (validateSequence(seq) != true)
                    {
                        throw new Exception(String.Format("Invalid sequence. Sequence expect {0} but received {1}", 
                            _client.Sequence, 
                            seq));
                    }
                    setSequence(seq);
                    _output.Add(new PacketData(parseRawData(_input)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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

        private bool validateSequence(byte _seq)
        {
            if (_client.Sequence == _seq) //if initial sequence
                return true;
            if (_client.Sequence + 1 == _seq) //if not initial
                return true;
            return false;
        }

        private void setSequence(byte _seq)
        {
            _client.Sequence = _seq;
        }

        private bool validateHeader(byte _seq)
        {
            return _seq == (byte)PacketSignature.TCPSignature;
        }
    }
}

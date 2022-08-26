using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class ByteUtil
    {
        public static String ByteBufToString(IByteBuffer _byteBuffer, int _size)
        {
            byte[] buffer = new byte[_size];
            _byteBuffer.ReadBytes(buffer);
            return Convert.ToHexString(buffer);
        }

        public static String ByteArrToString(byte[] _byteBuffer)
        {
            return Convert.ToHexString(_byteBuffer);
        }
    }
}

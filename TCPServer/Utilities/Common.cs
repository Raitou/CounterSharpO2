using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// TODO: Move to Common Library or something related as this will also be used by other Server Types
namespace TCPServer.Utilities
{
    internal static class Common
    {
        public static String ByteBufToString(IByteBuffer _byteBuffer, int _size)
        {
            byte[] buffer = new byte[_size];
            _byteBuffer.ReadBytes(buffer);
            return Convert.ToHexString(buffer);
        }
    }
}

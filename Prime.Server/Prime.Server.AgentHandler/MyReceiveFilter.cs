using System;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace Prime.Server
{
    class MyReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {
        protected MyReceiveFilter() 
            : base(38)
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            int size = (int)BitConverter.ToUInt32(header, header.Length - 4);
            return size;
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            byte[] commandByte = new byte[2];
            header.Array.CopyTo(commandByte, 0);
            string command = BitConverter.ToUInt16(commandByte, 0).ToString();
            BinaryRequestInfo bri = new BinaryRequestInfo(command, bodyBuffer);
            return bri;
        }
    }
}

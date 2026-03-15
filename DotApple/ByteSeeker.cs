using System;

namespace DotApple
{
    public class ByteSeeker(byte[] source)
    {
        private int _pointer = 0;

        public byte GetByte()
        {
            var v = source[_pointer];
            _pointer++;
            return v;
        }

        public ushort GetUshort()
        {
            var v = BitConverter.ToUInt16(source, _pointer);
            _pointer += 2;
            return v;
        }

        public uint GetUint()
        {

            var v = BitConverter.ToUInt32(source, _pointer);
            _pointer += 4;
            return v;
        }
    }
}
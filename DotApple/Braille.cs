using System.Text;

namespace DotApple
{
    public class Braille
    {
        public static char GetUnicodeChar(byte[] input)
        {
            var hex =
                Normalize(input[0]) * 0x1 +
                Normalize(input[1]) * 0x8 +
                Normalize(input[2]) * 0x2 +
                Normalize(input[3]) * 0x10 +
                Normalize(input[4]) * 0x4 +
                Normalize(input[5]) * 0x20 +
                Normalize(input[6]) * 0x40 +
                Normalize(input[7]) * 0x80;

            return (char)(0x2800+hex);
        }

        private static int Normalize(byte b) => b == 0 ? 0 : 1;

    }
}
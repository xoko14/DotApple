using System;
using System.Threading;
using System.Xml.Linq;

namespace DotApple
{
    public class Runner
    {
        public static void Run()
        {
            FConsole.Initialize("badapple");
            
            var seeker = new ByteSeeker(Binary.Data);

            var height = seeker.GetByte();
            var width = seeker.GetByte();
            var framerate = seeker.GetByte();
            var frameCount = seeker.GetUint();
            
            var frameTime = TimeSpan.FromMilliseconds(1000d/framerate);

            for (var currentFrame = 0; currentFrame < frameCount; currentFrame++)
            {
                var startTime = DateTime.Now;
                var frame = new byte[height*width];
                var sectionsInFrame = seeker.GetUshort();

                var frameIndex = 0;
                for (int i = 0; i < sectionsInFrame; i++)
                {
                    var color = seeker.GetByte();
                    var count = seeker.GetUshort();

                    for (int j = 0; j < count; j++)
                    {
                        frame[frameIndex] = color;
                        frameIndex++;
                    }
                }

                for (int y = 0; y < (height / 4); y++)
                {
                    for (int x = 0; x < (width / 2); x++)
                    {
                        var startPos = width * y * 4 + x * 2;
                        var brailleChar = Braille.GetUnicodeChar([
                            frame[startPos],
                            frame[startPos + 1],
                            frame[startPos + width],
                            frame[startPos + width + 1],
                            frame[startPos + width * 2],
                            frame[startPos + width * 2 + 1],
                            frame[startPos + width * 3],
                            frame[startPos + width * 3 + 1],
                        ]);
                        FConsole.SetChar((x, y), brailleChar, ConsoleColor.White, ConsoleColor.Black);
                    }

                }

                FConsole.DrawBuffer();
                
                var endTime = DateTime.Now;
                var timeTaken =  endTime - startTime;
                if (timeTaken < frameTime)
                {
                    Thread.Sleep(frameTime - timeTaken);
                }
            }
        }

        public static void Run(string arg)
        {
            if(arg == "badapple")
            {
                Runner.Run();
            }
        }
    }
}

using System.IO;

namespace WindowsFormsApp1
{
    class Environment
    {
        public const int width = 668;
        public const int height = 297;
        public static int x = 10;
        public const int MarioX = 280;
        public const int MarioY = 148;
        public static readonly string atackSound = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\atack3.wav";
        public static readonly string prepareSound = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\prepare.wav";
        public static readonly string json = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\best.json";
        public const int Mushroom1X = 407;
        public const int Mushroom1Y = 246;
        public const int Princess1X = 404;
        public const int Princess1Y = 235;
        public const int ExplosionX = 390;
        public const int ExplosionY = 223;


    }
}

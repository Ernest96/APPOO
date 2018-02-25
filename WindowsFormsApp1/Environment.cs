using System.IO;

namespace WindowsFormsApp1
{
    class Environment
    {
        public static readonly string atackSound = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\atack3.wav";
        public static readonly string prepareSound = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\prepare.wav";
        public static readonly string json = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\best.json";
        public const int width = 668;
        public const int height = 297;

        //LEVEL 1
        public const int Mario1X = 280;
        public const int Mario1Y = 148;
        public const int Mushroom1X = 407;
        public const int Mushroom1Y = 246;
        public const int Princess1X = 404;
        public const int Princess1Y = 235;
        public const int Explosion1X = 390;
        public const int Explosion1Y = 223;

        //LEVEL 2
        public const int Mario2X = 134;
        public const int Mario2Y = 139;
        public const int Mushroom2X = 259;
        public const int Mushroom2Y = 233;
        public const int Princess2X = 256;
        public const int Princess2Y = 222;
        public const int Dragon2X = 256;
        public const int Dragon2Y = 222;
        public const int Explosion2X = 244;
        public const int Explosion2Y = 212;
    }
}

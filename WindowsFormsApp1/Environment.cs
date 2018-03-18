using System.IO;

namespace WindowsFormsApp1
{
    // setarile jocului

    class Environment
    {
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
        public const int Explosion1X = 384;
        public const int Explosion1Y = 213;

        //LEVEL 2
        public const int Mario2X = 134;
        public const int Mario2Y = 139;
        public const int Mushroom2X = 259;
        public const int Mushroom2Y = 233;
        public const int Princess2X = 256;
        public const int Princess2Y = 222;
        public const int Dragon2X = 256;
        public const int Dragon2Y = 222;
        public const int Explosion2X = 234;
        public const int Explosion2Y = 202;

        //LEVEL 3
        public const int Mario3X = 130;
        public const int Mario3Y = 159;
        public const int Dragon3X = 740;
        public const int Dragon3Y = 222;
    }
}

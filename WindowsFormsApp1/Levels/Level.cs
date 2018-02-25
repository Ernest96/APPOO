using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    class Level
    {
        protected Game game;
        public List<NPC> NPCS;
        public Bitmap background;
        public int explosionX;
        public int explosionY;
        public int marioX;
        public int marioY;
        public int passPoints;
        public Bitmap explodeImg;

        public Level(Game game, int explosionX, int explosionY, int marioX, int marioY)
        {
            this.game = game;
            NPCS = new List<NPC>();

            explodeImg = Properties.Resources.explode;
            this.explosionX = explosionX;
            this.explosionY = explosionY;
            this.marioX = marioX;
            this.marioY = marioY;
        }

        public virtual NPC ChooseNpc()
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    // clasa sablon pentru orice nivel

    public class Level
    {
        public List<NPC> NPCS;
        public Bitmap background;
        public int explosionX;
        public int explosionY;
        public int marioX;
        public int marioY;
        public int passPoints;
        public Bitmap explodeImg;
        protected CancellationTokenSource logicToken;
        protected CancellationTokenSource generateToken;


        public Level(int explosionX, int explosionY, int marioX, int marioY)
        {
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

        public virtual void DrawLevel(Graphics g)
        {

        }
        
        protected virtual void Logic()
        {

        }

        protected virtual void Generate()
        {

        }

        public virtual void StartLevel()
        {
            
        }

        public void Stop()
        {
            logicToken.Cancel();
            generateToken.Cancel();
        }

    }
}

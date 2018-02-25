using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class NPC : Model
    {
        protected int explosionX;
        protected int explosionY;
        protected bool isShowing;
        protected int scoreHit;
        protected Game game;

        public virtual void Hide()
        {
            isShowing = false;
            Debug.WriteLine("NPC is hiding");
        }

        public virtual void Show()
        {
            isShowing = true;
            Debug.WriteLine("NPC is showing");
        }

        public NPC(Game game)
        {
            this.game = game;
            explosionX = Environment.Explosion1X;
            explosionY = Environment.Explosion1Y;
        }

        override public void Draw(Graphics g)
        {
            g.DrawImage(img, x, y);
        }

        public bool IsPresent()
        {
            return isShowing;
        }

        public virtual void isAtacked()
        {
            Debug.WriteLine("npc is atacked");
        }
    }
}

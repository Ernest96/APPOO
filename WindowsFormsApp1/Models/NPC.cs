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
        protected Bitmap explodeImg;
        protected int explosionX;
        protected int explosionY;
        protected bool isShowing;

        public NPC()
        {
            explodeImg = Properties.Resources.explode;
            explosionX = Environment.ExplosionX;
            explosionY = Environment.ExplosionY;
        }

        override public void Draw(Graphics g)
        {
            g.DrawImage(img, x, y);
        }

        public void Hide()
        {
            isShowing = false;
            Debug.WriteLine("NPC is hiding");
        }

        public void Show()
        {
            isShowing = true;
            Debug.WriteLine("NPC is showing");
        }

        public void DrawExplosion(Graphics g, bool explode)
        {
            if (explode)
            {
                g.DrawImage(explodeImg, explosionX, explosionY);
            }
        }

        public bool IsPresent()
        {
            return isShowing;
        }

    }
}

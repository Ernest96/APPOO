using System.Diagnostics;
using System.Drawing;

namespace WindowsFormsApp1.Models
{
    public class NPC : Model
    {
        protected int explosionX;
        protected int explosionY;
        protected bool isShowing;
        protected int scoreHit;

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

        public NPC(int x, int y, Bitmap img)
        {
            if (img != null)
            {
                this.img = img;
                this.rect.Size = this.img.Size;
            }

            this.rect.X = x;
            this.rect.Y = y;
            explosionX = Environment.Explosion1X;
            explosionY = Environment.Explosion1Y;
        }

        override public void Draw(Graphics g)
        {
            g.DrawImage(img, rect.X, rect.Y);
        }

        public bool IsPresent()
        {
            return isShowing;
        }

        public virtual int isAtacked()
        {
            Debug.WriteLine("npc is atacked");
            return 0;
        }
    }
}

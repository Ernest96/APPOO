using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public abstract class Model
    {
        protected Bitmap img;
        public Rectangle rect;

        abstract public void Draw(Graphics g);
    }
}

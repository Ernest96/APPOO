using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    abstract class Model
    {
        protected int x;
        protected int y;
        protected Bitmap img;

        abstract public void Draw(Graphics g);
    }
}

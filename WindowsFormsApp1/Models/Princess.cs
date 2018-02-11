using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Princess : NPC
    {
        public Princess()
        {
            this.img = Properties.Resources.princess_prepare;
            explodeImg = Properties.Resources.explode;
            this.x = Environment.Princess1X;
            this.y = Environment.Princess1Y;
        }
    }
}

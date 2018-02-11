using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Mushroom : NPC
    {
        public Mushroom()
        {
            this.img = Properties.Resources.mushroom_prepare;
            this.x = Environment.Mushroom1X;
            this.y = Environment.Mushroom1Y;
        }
    }
}

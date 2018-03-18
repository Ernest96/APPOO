using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Princess : NPC
    {
        public Princess(int x, int y) : base (x, y, Properties.Resources.princess_prepare)
        {
            scoreHit = -10;
        }

        public override void Hide()
        {
            isShowing = false;
            Debug.WriteLine("Princess is hiding");
        }

        public override void Show()
        {
            isShowing = true;
            Debug.WriteLine("Princess is showing");
        }

        public override int isAtacked()
        {
            return scoreHit;
        }
    }
}

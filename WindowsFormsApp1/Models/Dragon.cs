using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Dragon: NPC
    {
        public Dragon(int x, int y) : base(x, y, Properties.Resources.dragon)
        {
            scoreHit = 7;
        }

        public override void Hide()
        {
            isShowing = false;
            Debug.WriteLine("Dragon is hiding");
        }

        public override void Show()
        {
            isShowing = true;
            Debug.WriteLine("Dragon is showing");
        }

        public override int isAtacked()
        {
            return scoreHit;
        }
    }
}

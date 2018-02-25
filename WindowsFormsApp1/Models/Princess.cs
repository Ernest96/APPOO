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
        public Princess(Game game, int x, int y) : base (game)
        {
            this.img = Properties.Resources.princess_prepare;
            this.x = x;
            this.y = y;
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

        public override void isAtacked()
        {
            game.score = game.score + scoreHit;
        }
    }
}

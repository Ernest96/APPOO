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
        public Dragon(Game game, int x, int y) : base(game)
        {
            this.img = Properties.Resources.dragon;
            this.x = x;
            this.y = y;
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

        public override void isAtacked()
        {
            game.score = game.score + scoreHit;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Mario : Model
    {
        public  bool isPreparing { get; private set; }
        public  bool isAtacking { get; private set; }
        private  Bitmap prepareImg;
        private Bitmap atackImg;
        private Bitmap stayImg;
        private Game game;

        override public void Draw(Graphics g)
        {
            g.DrawImage(img, x, y);
        }

        public Mario(Game game)
        {
            x = game.currentLevel.marioX;
            y = game.currentLevel.marioY;
            img = Properties.Resources.mario_stay;
            prepareImg = Properties.Resources.mario_prep;
            atackImg = Properties.Resources.mario_action;
            stayImg = Properties.Resources.mario_stay;
        }

        public void Atack()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.atackSound);
            player.Play();
            this.img = atackImg;
            isPreparing = false;
            isAtacking = true;
        }

        public void Prepare()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.prepareSound);
            player.Play();
            this.img = prepareImg;
            isPreparing = true;
            isAtacking = false;
        }

        public void Stay()
        {
            this.img = stayImg;
            isPreparing = false;
            isAtacking = false;
        }
    }
}
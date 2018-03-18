using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Mario : Model
    {
        public bool isPreparing { get; private set; }
        public bool isAtacking { get; private set; }
        public bool isJumping { get; private set; }
        public bool isAtacked { get; private set; }

        override public void Draw(Graphics g)
        {
            g.DrawImage(img, rect.X, rect.Y);
        }

        public Mario(int x, int y)
        {
            img = Properties.Resources.mario_stay;
            this.rect.X = x;
            this.rect.Y = y;
            this.rect.Size = img.Size;
            var a = this.rect.IntersectsWith(this.rect);
            ;
            ;
        }

        public void Atack()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.atack3);
            player.Play();
            this.img = Properties.Resources.mario_action;
            isPreparing = false;
            isAtacking = true;
        }

        public void Jump(int height)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.jump);
            player.Play();
            this.isJumping = true;
            this.img = Properties.Resources.mario_jump;
            this.rect.Y -= height;
        }

        public void Fall()
        {
            this.img = Properties.Resources.mario_stay;
            this.rect.Y = Environment.Mario3Y;
            this.isJumping = false;
            this.isAtacked = false;
            Thread.Sleep(50);
        }

        public void Prepare()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.prepare);
            player.Play();
            this.img = Properties.Resources.mario_prep;
            isPreparing = true;
            isAtacking = false;
        }

        public void Stay()
        {
            this.img = Properties.Resources.mario_stay;
            isPreparing = false;
            isAtacking = false;
            isJumping = false;
            isAtacked = false;
        }

        public void Atacked()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.stomp);
            player.Play();
            this.img = Properties.Resources.mario_atacked;
            isJumping = false;
            isAtacked = true;
        }
    }
}
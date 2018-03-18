using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Levels
{
    class Level3 : Level
    {
        private bool escaped = true;

        public Level3() :
            base(Environment.Explosion1X, Environment.Explosion1Y, Environment.Mario3X, Environment.Mario3Y)
        {
            logicToken = new CancellationTokenSource();
            generateToken = new CancellationTokenSource();
            background = Properties.Resources.back3;
            passPoints = 4000;
        }

        //interface DRAWABLE
        public override void DrawLevel(Graphics g)
        {
            Bitmap back = Game.Instance.currentLevel.background;
            g.DrawImage(back, 0, 0, back.Width, back.Height);
            Game.Instance.mario.Draw(g);
            if (Game.Instance.npc.IsPresent())
            {
                Game.Instance.npc.Draw(g);
            }
            if (Game.Instance.isExplode)
            {
                g.DrawImage(Game.Instance.currentLevel.explodeImg, Game.Instance.currentLevel.explosionX, Game.Instance.currentLevel.explosionY);
            }
        }

        public override NPC ChooseNpc()
        {
            return new Dragon(Environment.Dragon3X, Environment.Dragon3Y);
        }

        protected override void Logic()
        {
            CancellationToken ct = logicToken.Token;
            long startTime = System.Environment.TickCount;
            Thread.Sleep(100);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (logicToken.IsCancellationRequested)
                    {
                        Console.WriteLine("task canceled");
                        break;
                    }

                    if (Game.Instance.keyIsPressed && !Game.Instance.mario.isJumping)
                    {
                        Game.Instance.mario.Jump(70);
                        startTime = System.Environment.TickCount;
                        Game.Instance.keyIsPressed = false;
                    }

                    if (System.Environment.TickCount >= startTime + 350)
                    {
                        if (Game.Instance.mario.isJumping)
                        {
                            Game.Instance.mario.Fall();
                        }
                        else if (Game.Instance.mario.isAtacked)
                        {
                            Game.Instance.mario.Fall();
                        }
                    }

                    if (Game.Instance.npc.rect.IntersectsWith(Game.Instance.mario.rect) && !Game.Instance.mario.isAtacked)
                    {
                        escaped = false;
                        Game.Instance.mario.Atacked();
                        Thread.Sleep(500);
                    }
                }
            }, ct);
        }

        protected override void Generate()
        {
            CancellationToken ct = generateToken.Token;
            long startTime = System.Environment.TickCount;
            long moveTime = 40;
            Thread.Sleep(100);

            Game.Instance.npc = ChooseNpc();
            Game.Instance.npc.Show();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (generateToken.IsCancellationRequested)
                    {
                        Console.WriteLine("task canceled");
                        break;
                    }

                    if (System.Environment.TickCount >= startTime + moveTime)
                    {
                        Game.Instance.npc.rect.X -= 30;
                        startTime = System.Environment.TickCount;
                    }
                    if (Game.Instance.npc.rect.Right < 0)
                    {
                        if (escaped == true)
                        {
                            Game.Instance.EscapeNpc();
                        }

                        int nextTime;
                        Random random = new Random();
                        nextTime = random.Next(100, 500);
                        Thread.Sleep(nextTime);
                        Game.Instance.npc = ChooseNpc();
                        Game.Instance.npc.Show();
                        escaped = true;
                    }
                }
            }, ct);
        }

        public override void StartLevel()
        {
            Task.Run(() => Logic());
            Task.Run(() => Generate());
        }
    }
}

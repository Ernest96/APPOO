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
            mario.Draw(g);
            if (npc.IsPresent())
            {
                npc.Draw(g);
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

                    if (keyIsPressed && !mario.isJumping)
                    {
                        mario.Jump(70);
                        startTime = System.Environment.TickCount;
                        keyIsPressed = false;
                    }

                    if (System.Environment.TickCount >= startTime + 350)
                    {
                        if (mario.isJumping)
                        {
                            mario.Fall();
                        }
                        else if (mario.isAtacked)
                        {
                            mario.Fall();
                        }
                    }

                    if (npc.rect.IntersectsWith(mario.rect) && !mario.isAtacked)
                    {
                        escaped = false;
                        mario.Atacked();
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

            npc = ChooseNpc();
            npc.Show();

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
                        npc.rect.X -= 30;
                        startTime = System.Environment.TickCount;
                    }
                    if (npc.rect.Right < 0)
                    {
                        if (escaped == true)
                        {
                            EscapeNpc();
                        }

                        int nextTime;
                        Random random = new Random();
                        nextTime = random.Next(100, 500);
                        Thread.Sleep(nextTime);
                        npc = ChooseNpc();
                        npc.Show();
                        escaped = true;
                    }
                }
            }, ct);
        }

        public void EscapeNpc()
        {
            Game.Instance.ChangeScore(npc.isAtacked());
        }

        public override void StartLevel()
        {
            Task.Run(() => Logic());
            Task.Run(() => Generate());
        }
    }
}

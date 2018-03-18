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
    // nivelul 2
    class Level2 : Level
    {
        public Level2() :
            base(Environment.Explosion2X, Environment.Explosion2Y, Environment.Mario2X, Environment.Mario2Y)
        {
            logicToken = new CancellationTokenSource();
            generateToken = new CancellationTokenSource();
            background = Properties.Resources.back2;
            passPoints = 8;
            NPCS.Add(new Princess(Environment.Princess2X, Environment.Princess2Y));
            NPCS.Add(new Mushroom(Environment.Mushroom2X, Environment.Mushroom2Y));
            NPCS.Add(new Dragon(Environment.Dragon2X, Environment.Dragon2Y));
        }

        public override NPC ChooseNpc()
        {
            Random random = new Random();
            var r = random.Next(1, 100);

            if (r >= 65)
                return NPCS.ElementAt(0);
            else if (r >=25)
                return NPCS.ElementAt(1);
            else
                return NPCS.ElementAt(2);
        }

        public override void DrawLevel(Graphics g)
        {
            Bitmap back = Game.Instance.currentLevel.background;
            g.DrawImage(back, 0, 0, back.Width, back.Height);
            mario.Draw(g);
            if (npc.IsPresent())
            {
                npc.Draw(g);
            }
            if (isExplode)
            {
                g.DrawImage(Game.Instance.currentLevel.explodeImg, Game.Instance.currentLevel.explosionX, Game.Instance.currentLevel.explosionY);
            }
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
                        break;
                    }

                    if (keyIsPressed)
                    {
                        mario.Prepare();
                        startTime = System.Environment.TickCount;
                        keyIsPressed = false;
                    }

                    if (System.Environment.TickCount >= startTime + 450)
                    {
                        if (mario.isPreparing)
                        {
                            mario.Atack();
                            if (npc.IsPresent())
                            {
                                AtackNpc();
                                npc.Hide();
                            }
                            startTime = System.Environment.TickCount;
                        }
                        else if (mario.isAtacking)
                        {
                            isExplode = false;
                            mario.Stay();
                            startTime = System.Environment.TickCount;
                        }
                    }
                }
            }, ct);
        }

        public void AtackNpc()
        {
            isExplode = true;
            Game.Instance.ChangeScore(npc.isAtacked());
        }

        protected override void Generate()
        {
            CancellationToken ct = generateToken.Token;
            long startTime = System.Environment.TickCount;
            long nextTime = 800;
            long stayTime = 900;
            Thread.Sleep(100);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (generateToken.IsCancellationRequested)
                    {
                        break;
                    }

                    if (System.Environment.TickCount >= startTime + nextTime && !npc.IsPresent())
                    {
                        npc = ChooseNpc();
                        npc.Show();
                        startTime = System.Environment.TickCount;
                        Random random = new Random();
                        nextTime = random.Next(700, 3500);
                    }
                    if (System.Environment.TickCount >= startTime + stayTime && npc.IsPresent())
                    {
                        npc.Hide();
                        startTime = System.Environment.TickCount;
                        Random random = new Random();
                        stayTime = random.Next(820, 1400);
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

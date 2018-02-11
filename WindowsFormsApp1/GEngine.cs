using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    class GEngine
    {
        private Graphics drawHandle;
        private Thread renderThread;
        private Thread logicThread;
        private Thread generateThread;
        private Game game;

        public GEngine(Graphics g, Game game)
        {
            drawHandle = g;
            this.game = game;
        }

        public void init()
        {
            renderThread = new Thread(new ThreadStart(render));
            logicThread = new Thread(new ThreadStart(logic));
            generateThread = new Thread(new ThreadStart(generate));
            renderThread.Start();
            logicThread.Start();
            generateThread.Start();
        }

        public void stop()
        {
            renderThread.Abort();
        }

        private void generate()
        {
            long startTime = System.Environment.TickCount;
            long nextTime = 800;
            long stayTime = 900;

            while (true)
            {
                if (System.Environment.TickCount >= startTime + nextTime && !game.NpcIsPresent())
                {
                    game.ShowNpc();
                    startTime = System.Environment.TickCount;
                    Random random = new Random();
                    nextTime = random.Next(700, 3500);
                }
                if (System.Environment.TickCount >= startTime + stayTime && game.NpcIsPresent())
                {
                    game.HideNpc();
                    startTime = System.Environment.TickCount;
                    Random random = new Random();
                    stayTime = random.Next(820, 1400);
                }
            }
           
        }

        private void logic()
        {
            long startTime = System.Environment.TickCount;
            
            while (true)
            {
                if (game.keyIsPressed)
                {
                    game.PrepareMario();
                    startTime = System.Environment.TickCount;
                    game.keyIsPressed = false;
                }

                if (System.Environment.TickCount >= startTime + 450)
                {
                    if (game.MarioIsPreparing())
                    {
                        game.MarioAtack();
                        if (game.NpcIsPresent())
                        {
                            game.AtackNpc();
                            game.HideNpc();
                        }
                        startTime = System.Environment.TickCount;
                    }
                    else if (game.MarioIsAtacking())
                    {
                        game.MarioStay();
                        startTime = System.Environment.TickCount;
                    }
                }

                
            }
        }

        private void render()
        {
            Bitmap frame = new Bitmap(Environment.width, Environment.height);
            Graphics temp = Graphics.FromImage(frame);
            Bitmap back = Properties.Resources.back;

            while (true)
            {
                temp.DrawImage(back, 0, 0);
                temp.DrawString("Score = " + game.score,
                                new System.Drawing.Font("Arial", 14),
                                new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                5, 26
                                );
                temp.DrawString("Best Score = " + game.bestScore,
                                new System.Drawing.Font("Arial", 14),
                                new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                5, 5
                                );
                game.DrawNpc(temp);
                game.DrawMario(temp);
                game.DrawExplosion(temp);

                drawHandle.DrawImage(frame, 0, 0);

            }
        }
    }
}

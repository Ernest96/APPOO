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
    class GraphicEngine
    {
        private Graphics drawHandle;
        private Thread renderThread;
        private Game game;

        public GraphicEngine(Graphics g, Game game)
        {
            drawHandle = g;
            this.game = game;
            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();
        }

        private void render()
        {
            Bitmap frame = new Bitmap(Environment.width, Environment.height);
            Graphics temp = Graphics.FromImage(frame);

            while (true)
            {
                Bitmap back = game.currentLevel.background;
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
                temp.DrawString("Level = " + (game.levelIndex + 1),
                               new System.Drawing.Font("Arial", 14),
                               new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                               580, 5
                               );
                game.DrawNpc(temp);
                game.DrawMario(temp);
                game.DrawExplosion(temp);

                drawHandle.DrawImage(frame, 0, 0);
            }
        }

    }
}

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
        // clasa pentru desenarea elementelor grafice

        private Graphics drawHandle;
        private Thread renderThread;

        public GraphicEngine(Graphics g)
        {
            drawHandle = g;
            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();
        }

        private void render()
        {
            Bitmap frame = new Bitmap(Environment.width, Environment.height);
            Graphics temp = Graphics.FromImage(frame);

            while (true)
            {
                Game.Instance.currentLevel.DrawLevel(temp);

                temp.DrawString("Score = " + Game.Instance.score,
                                new System.Drawing.Font("Arial", 14),
                                new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                5, 26
                                );
                temp.DrawString("Best Score = " + Game.Instance.bestScore,
                                new System.Drawing.Font("Arial", 14),
                                new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                5, 5
                                );
                temp.DrawString("Level = " + (Game.Instance.levelIndex + 1),
                               new System.Drawing.Font("Arial", 14),
                               new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                               560, 5
                               );

                drawHandle.DrawImage(frame, 0, 0);
            }
        }

    }
}

using System.Drawing;
using System.Threading;
using WindowsFormsApp1.Interfaces;

namespace WindowsFormsApp1
{
    class GraphicEngine : IDrawable
    { 
        // clasa pentru desenarea elementelor grafice
        private Graphics drawHandle;
        private Thread renderThread;
        private bool isRendering = false;

        public GraphicEngine(Graphics g)
        {
            drawHandle = g;
        }

        public void StartDraw()
        {
            if (isRendering)
                return;

            isRendering = true;
            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();
        }

        public void StopDraw()
        {
            isRendering = false;
            renderThread.Abort();
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

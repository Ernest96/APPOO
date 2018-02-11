using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsFormsApp1.Models;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace WindowsFormsApp1
{
    class Game
    {
        private GEngine gEngine;
        private Mario mario;
        private NPC npc = new Princess();
        private Thread fileThread;
        public int score { get; private set; }
        public int bestScore { get; private set; }
        public bool keyIsPressed = false;
        private bool isExplode = false;

        public Game()
        {
            fileThread = new Thread(new ThreadStart(getScore));
            fileThread.Start();
        }

        private void getScore()
        {
            using (StreamReader file = File.OpenText(Environment.json))
            {
                JsonSerializer serializer = new JsonSerializer();
                bestScore = (int)serializer.Deserialize(file, typeof(int));
            }
            fileThread.Abort();
        }

        private void setBestScore()
        {
            using (StreamWriter writetext = new StreamWriter(Environment.json))
            {
                writetext.Write(bestScore.ToString());
            }
            fileThread.Abort();

        }

        public void startGraphics(Graphics g)
        {
            gEngine = new GEngine(g, this);
            mario = new Mario();
            gEngine.init();
        }

        public void DrawMario(Graphics g)
        {
            mario.Draw(g);
        }

        public bool MarioIsPreparing()
        {
            return mario.isPreparing;
        }

        public bool MarioIsAtacking()
        {
            return mario.isAtacking;
        }

        public void PrepareMario()
        {
            this.mario.Prepare();
        }

        public void MarioAtack()
        {
            this.mario.Atack();
        }

        public void MarioStay()
        {
            isExplode = false;
            this.mario.Stay();
        }

        public void stopGame()
        {
            gEngine.stop();
        }

        public void DrawNpc(Graphics g)
        {
            if (npc.IsPresent())
            {
                npc.Draw(g);
            }
        }

        public bool NpcIsPresent()
        {
            return npc.IsPresent();
        }

        public void DrawExplosion(Graphics g)
        {
            npc.DrawExplosion(g, isExplode);
        }

        public void ShowNpc()
        {
            npc.Show();
        }

        public void HideNpc()
        {
            npc.Hide();
            Random random = new Random();
            var r = random.Next(1, 100);

            if (r >= 75)
                npc = new Princess();
            else
                npc = new Mushroom();
        }

        

        public void AtackNpc()
        {
            isExplode = true;
            if (npc is Mushroom)
            {
                score = score + 2;

                if (score > bestScore)
                {
                    bestScore = score;
                    fileThread = new Thread(new ThreadStart(setBestScore));
                    fileThread.Start();
                }
            }
            else if(npc is Princess)
            {
                score = score - 10;
            }
        }
    }
}

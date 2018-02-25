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
using WindowsFormsApp1.Levels;

namespace WindowsFormsApp1
{
    class Game
    {
        private GraphicEngine graphicEngine;
        private Mario mario;
        private NPC npc;
        public int score { get; set; }
        public int bestScore { get; private set; }
        public bool keyIsPressed = false;
        private bool isExplode = false;
        public Level currentLevel;
        public int levelIndex { get; private set; }
        private List<Level> Levels;

        public Game()
        {
            Levels = new List<Level>();
            Levels.Add(new Level1(this));
            Levels.Add(new Level2(this));
            levelIndex = 0;
            currentLevel = Levels.ElementAt(levelIndex);
            mario = new Mario(this);
            npc = new NPC(this);
            Task.Run(() => getScore());
            Task.Run(() => logic());
            Task.Run(() => generate());
        }

        public void startGraphics(Graphics g)
        {
            graphicEngine = new GraphicEngine(g, this);
        }

        public void ChangeLevel()
        {
            if (levelIndex < Levels.Count - 1)
                levelIndex++;

            currentLevel = Levels.ElementAt(levelIndex);
            isExplode = false;
            mario = new Mario(this);
        }

        private void generate()
        {
            long startTime = System.Environment.TickCount;
            long nextTime = 800;
            long stayTime = 900;

            while (true)
            {
                if (System.Environment.TickCount >= startTime + nextTime && !npc.IsPresent())
                {
                    ShowNpc();
                    startTime = System.Environment.TickCount;
                    Random random = new Random();
                    nextTime = random.Next(700, 3500);
                }
                if (System.Environment.TickCount >= startTime + stayTime && npc.IsPresent())
                {
                    HideNpc();
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
                            HideNpc();
                        }
                        startTime = System.Environment.TickCount;
                    }
                    else if (mario.isAtacking)
                    {
                        isExplode = false;
                        this.mario.Stay();
                        startTime = System.Environment.TickCount;
                    }
                }
            }
        }

        public void ShowNpc()
        {
            npc = currentLevel.ChooseNpc();
            npc.Show();
        }

        public void HideNpc()
        {
            npc.Hide();
        }

        public void AtackNpc()
        {
            isExplode = true;

            npc.isAtacked();

            if (score > bestScore)
            {
                bestScore = score;
                Task.Run(() => setBestScore());
            }

            if (score >= currentLevel.passPoints)
            {
                ChangeLevel();
            }

        }

        public void DrawMario(Graphics g)
        {
            mario.Draw(g);
        }

        public void DrawNpc(Graphics g)
        {
            if (npc.IsPresent())
            {
                npc.Draw(g);
            }
        }

        public void DrawExplosion(Graphics g)
        {
            if (isExplode)
            {
                g.DrawImage(currentLevel.explodeImg, currentLevel.explosionX, currentLevel.explosionY);
            }
        }

        private void getScore()
        {
            if (File.Exists(Environment.json))
            {
                using (StreamReader file = File.OpenText(Environment.json))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    bestScore = (int)serializer.Deserialize(file, typeof(int));
                }
            }
        }

        private void setBestScore()
        {
            using (StreamWriter writetext = new StreamWriter(Environment.json))
            {
                writetext.Write(bestScore.ToString());
            }

        }
    }
}

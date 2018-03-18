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

// de respectat SOLID!!!
// DE FOLOSIT INTERFETE
namespace WindowsFormsApp1
{
    // clasa de baza (joaca)
    public sealed class Game
    {
        private static readonly object _mutex = new object();
        private static Game _instance;
        private GraphicEngine graphicEngine;
        //de inchis in nivel 
        public Mario mario;
        public NPC npc;
        ////
        public int score { get; set; }
        public int bestScore { get; private set; }
        public bool keyIsPressed = false;
        public bool isExplode;
        public Level currentLevel;
        public int levelIndex { get; private set; }
        private List<Level> Levels;

        public static Game Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new Game();
                        }
                    }
                }
                return _instance;
            }
        }

        private Game()
        {
            Levels = new List<Level>();
            Levels.Add(new Level1());
            Levels.Add(new Level2());
            Levels.Add(new Level3());
            levelIndex = 0;
            currentLevel = Levels.ElementAt(levelIndex);
            npc = new NPC(0, 0, null);
            mario = new Mario(currentLevel.marioX, currentLevel.marioY);
            Task.Run(() => getScore());
            currentLevel.StartLevel();
        }

        public void startGraphics(Graphics g)
        {
            graphicEngine = new GraphicEngine(g);
        }

        public void ChangeLevel()
        {
            if (levelIndex < Levels.Count - 1)
                levelIndex++;

            currentLevel.Stop();
            currentLevel = Levels.ElementAt(levelIndex);
            currentLevel.StartLevel();
            mario = new Mario(currentLevel.marioX, currentLevel.marioY);
            isExplode = false;
        }

        public void HideNpc()
        {
            npc.Hide();
        }

        public void AtackNpc()
        {
            isExplode = true;
            score += npc.isAtacked();
            CheckScore();
        }

        public void EscapeNpc()
        {
            score += npc.isAtacked();
            CheckScore();
        }

        private void CheckScore()
        {
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

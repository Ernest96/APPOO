using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using WindowsFormsApp1.Levels;
using WindowsFormsApp1.Interfaces;

namespace WindowsFormsApp1
{
    // clasa de baza (joaca)
    public sealed class Game
    {
        private static readonly object _mutex = new object();
        private static Game _instance;
        private IDrawable graphicEngine;
        public int score { get; set; }
        public int bestScore { get; private set; }
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
            currentLevel.StartLevel();
            Task.Run(() => getScore());
        }

        public void startGraphics(Graphics g)
        {
            graphicEngine = new GraphicEngine(g);
            graphicEngine.StartDraw();
        }

        public void stopGraphics()
        {
            graphicEngine.StopDraw();
        }

        public void ChangeLevel()
        {
            if (levelIndex < Levels.Count - 1)
                levelIndex++;

            currentLevel.Stop();
            currentLevel = Levels.ElementAt(levelIndex);
            currentLevel.StartLevel();
        }

        public void ChangeScore(int scoreAdd)
        {
            score += scoreAdd;

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

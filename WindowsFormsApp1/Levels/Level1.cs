using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Levels
{
    class Level1 : Level
    {
        public Level1(Game game) :
            base(game, Environment.Explosion1X, Environment.Explosion1Y, Environment.Mario1X, Environment.Mario1Y)
        {
            background = Properties.Resources.back;
            passPoints = 8;
            NPCS.Add(new Princess(game, Environment.Princess1X, Environment.Princess1Y));
            NPCS.Add(new Mushroom(game, Environment.Mushroom1X, Environment.Mushroom1Y));
        }

        public override NPC ChooseNpc()
        {
            Random random = new Random();
            var r = random.Next(1, 100);

            if (r >= 75)
                return NPCS.ElementAt(0);
            else
                return NPCS.ElementAt(1);
        }
    }
}

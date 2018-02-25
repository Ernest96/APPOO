using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Levels
{
    class Level2 : Level
    {
        public Level2(Game game) :
            base(game, Environment.Explosion2X, Environment.Explosion2Y, Environment.Mario2X, Environment.Mario2Y)
        {
            background = Properties.Resources.back2;
            passPoints = 1000;
            NPCS.Add(new Princess(game, Environment.Princess2X, Environment.Princess2Y));
            NPCS.Add(new Mushroom(game, Environment.Mushroom2X, Environment.Mushroom2Y));
            NPCS.Add(new Dragon(game, Environment.Dragon2X, Environment.Dragon2Y));
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
    }
}

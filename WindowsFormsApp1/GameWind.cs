using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class canvas : Form
    {
        private Game game = new Game();

        public canvas()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            game.startGraphics(g);
        }

        private void canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.stopGame();
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                game.keyIsPressed = true;
            }
        }
    }
}

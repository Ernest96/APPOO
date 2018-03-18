using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //fereastra aplicatiei (forma)

    public partial class canvas : Form
    {
        public canvas()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            var a = Game.Instance;
            Game.Instance.startGraphics(g);
        }

        private void canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Instance.stopGraphics();
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Game.Instance.currentLevel.keyIsPressed = true;
            }
        }
    }
}

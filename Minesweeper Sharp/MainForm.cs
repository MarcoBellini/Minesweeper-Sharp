using Minesweeper_Sharp.Engine;

namespace Minesweeper_Sharp
{
    public partial class MainForm : Form
    {
        GameManager? GameManager = null;
        Game_Level Current_Level = Game_Level.Beginner;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Prepare_New_Game()
        {
            GameManager?.Dispose();
            GameManager = new(Current_Level, this);
            Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Prepare_New_Game();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            GameManager?.Draw(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            GameManager?.MouseEvent(e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prepare_New_Game();
        }


        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            expertToolStripMenuItem.Checked = false;
            intermediateToolStripMenuItem.Checked = false;
            beginnerToolStripMenuItem.Checked = true;

            Current_Level = Game_Level.Beginner;
            Prepare_New_Game();
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            expertToolStripMenuItem.Checked = false;
            intermediateToolStripMenuItem.Checked = true;
            beginnerToolStripMenuItem.Checked = false;

            Current_Level = Game_Level.Intermediate;
            Prepare_New_Game();
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            expertToolStripMenuItem.Checked = true;
            intermediateToolStripMenuItem.Checked = false;
            beginnerToolStripMenuItem.Checked = false;

            Current_Level = Game_Level.Expert;
            Prepare_New_Game();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var AboutForm = new AboutForm();

            AboutForm.ShowDialog();
            AboutForm.Dispose();
        }
    }
}

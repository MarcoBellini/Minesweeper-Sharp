/*
MIT License

Copyright (c) 2024 Marco Bellini

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

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

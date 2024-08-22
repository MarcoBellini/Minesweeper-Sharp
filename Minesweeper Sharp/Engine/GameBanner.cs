
namespace Minesweeper_Sharp.Engine
{
    /// <summary>
    /// Display a Text banner inside the game (Win or Loose)
    /// </summary>
    public class GameBanner : IGameObject, IDisposable
    {

        // Font and format for Text function
        private readonly Font Current_Font = new Font("Segoe UI", 12, FontStyle.Bold);
        private StringFormat Current_Format = new();

        /// <summary>
        /// Banner Tenxt
        /// </summary>
        public string Banner_Text { get; set; } = string.Empty;

        /// <summary>
        /// Get Banner Rect
        /// </summary>
        public Rectangle Banner_Rectangle { get; private set; }

        /// <summary>
        /// Show or Hide Banner
        /// </summary>
        public bool Banner_Visible { get; set; } = false;

        /// <summary>
        /// Change color of banner
        /// </summary>
        public Brush Banner_Brush { get; set; } = Brushes.Black;

        /// <summary>
        /// Create a new Banner
        /// </summary>
        /// <param name="Position">Position in the game...</param>
        public GameBanner(Rectangle Position)
        {
            Banner_Rectangle = Position;

            // Center text to a Rectangle
            Current_Format.LineAlignment = StringAlignment.Near;
            Current_Format.Alignment = StringAlignment.Near;
        }

        public void Dispose()
        {
            Current_Font.Dispose();
            Current_Format.Dispose();           
        }

        public void Draw(PaintEventArgs g)
        {
            if (!Banner_Visible)
                return;

            g.Graphics.DrawString(Banner_Text, Current_Font, Banner_Brush, Banner_Rectangle, Current_Format);
        }

        public void MouseEvent(MouseEventArgs e)
        {
            // Nothing to do here..
        }
    }
}

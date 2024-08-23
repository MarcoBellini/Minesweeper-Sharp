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

using System.Drawing.Drawing2D;

namespace Minesweeper_Sharp.Engine
{
    public class GameCell : BaseComponent, IGameObject, IDisposable
    {
        // Size and location of the cell
        private readonly Rectangle Rect;

        // Font and format for Text function
        private readonly Font Current_Font = new Font("Segoe UI", 11, FontStyle.Bold);
        private StringFormat Current_Format = new();

        // Used to store a map of all colors
        private readonly Dictionary<int, Brush> Color_Map = new Dictionary<int, Brush>();

        /// <summary>
        /// Index of current cell (ReadOnly)
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Index of current cell (ReadOnly)
        /// </summary>
        public bool Use_Even_Color { get; }

        /// <summary>
        /// If <c>true</c> check this cell as a Mine
        /// </summary>
        public bool Is_A_Mine { get; set; } = false;

        /// <summary>
        /// If <see cref="Is_A_Mine"/> is set to <c>false</c> use this property to count nearby mines
        /// </summary>
        public int Nearby_Mines_Count { get; set; } = 0;

        /// <summary>
        /// If <c>true</c> show content
        /// </summary>
        public bool Show_Content { get; set; } = false;

        /// <summary>
        /// If <c>true</c> show a flag and prevent the user from click the cell
        /// </summary>
        public bool Show_Flag { get; set; } = false;

        /// <summary>
        /// If <c>true</c> an user clicked on this cell set as a mine
        /// </summary>
        public bool Is_Mine_Boom { get; private set; } = false;

        /// <summary>
        /// Return the rectangle of the current cell
        /// </summary>
        public Rectangle Cell_Rect { get => Rect; }

        /// <summary>
        /// Create new cell object
        /// </summary>
        /// <param name="Idx">Index of a GameCell</param>
        /// <param name="Rc">Size and posizion of a GameCell</param>
        /// <param name="Is_Even_Color">Use a different color. This create a checkered pattern.</param>
        public GameCell(int Idx, Rectangle Rc, bool Is_Even_Color = false) : base(null)
        {      
            Index = Idx;
            Rect = Rc; 
            Use_Even_Color = Is_Even_Color;

            // Center text to a Rectangle
            Current_Format.LineAlignment = StringAlignment.Center;
            Current_Format.Alignment = StringAlignment.Center;

            // Store cached brusched for each number
            Color_Map.Add(1, Brushes.Blue);
            Color_Map.Add(2, Brushes.Green);
            Color_Map.Add(3, Brushes.Red);
            Color_Map.Add(4, Brushes.DarkBlue);
            Color_Map.Add(5, Brushes.DarkRed);
            Color_Map.Add(6, Brushes.Coral);
            Color_Map.Add(7, Brushes.Black);
            Color_Map.Add(8, Brushes.DarkOrchid);            
        }

        /// <summary>
        /// Draw a Flag without background
        /// </summary>
        /// <param name="g">Ref to a Graphics object</param>
        private void DrawFlag(Graphics g)
        {
            int Triangle_Pad_X, Triangle_Pad_Y, Triangle_Half_Y, Triangle_Quart_Y, Pole_Padding_X, Pole_Padding_Y;
            GraphicsPath path = new();
            Rectangle Pole_Rect;
            Point p1, p2, p3;

            // Padding from left-right and top-bottom margins
            Triangle_Pad_X = Rect.Width * 25 / 100;     // % of Width
            Triangle_Pad_Y =  Rect.Height * 10 / 100;   // % of Height

            // Right middle point of the triangle
            Triangle_Half_Y = Rect.Height / 2 + Triangle_Pad_Y;
            Triangle_Quart_Y = Triangle_Half_Y / 2;

            // Flag Pole Padding
            Pole_Padding_X = Rect.Width * 10 / 100;  // % of Width
            Pole_Padding_Y = Rect.Height * 8 / 100; // % of Height

            p1 = new Point(Rect.Left + Triangle_Pad_X, Rect.Top + Triangle_Pad_Y);
            p2 = new Point(Rect.Right - Triangle_Pad_X, Rect.Top + Triangle_Quart_Y);
            p3 = new Point(Rect.Left + Triangle_Pad_X, Rect.Top + Triangle_Half_Y);

            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            path.AddLine(p3, p1);

            Pole_Rect = new Rectangle(Rect.Left + Triangle_Pad_X - Pole_Padding_X, Rect.Top + Pole_Padding_Y, Pole_Padding_X, Rect.Height - Pole_Padding_Y * 2);
            
            g.FillRectangle(Brushes.Black, Pole_Rect); // Flag Pole 
            g.FillPath(Brushes.Red, path);  // Flag Triangle

            // Mark wrong flags
            if ((!Is_A_Mine) && (Show_Content))
            {
                g.DrawLine(Pens.Red, Rect.Left, Rect.Top, Rect.Right, Rect.Bottom);
                g.DrawLine(Pens.Red, Rect.Right, Rect.Top, Rect.Left, Rect.Bottom);
            }

        }

        /// <summary>
        /// Draw a Mine without background
        /// </summary>
        /// <param name="g">Ref to a Graphics object</param>
        private void DrawMine(Graphics g)
        {
            int Padding = ((Rect.Width +  Rect.Height) / 2) * 25 / 100;
            Rectangle Mine = new(Rect.X, Rect.Y, Rect.Width, Rect.Height);

            // Reduce size subtracting padding
            Mine.Inflate(-Padding, -Padding);       

            g.FillEllipse(Brushes.Red, Mine);
        }

        public void Draw(PaintEventArgs e)
        {
            Brush Standard_Brush = Use_Even_Color ? Brushes.LightGreen : Brushes.PaleGreen;
            Brush Clicked_Brush = Use_Even_Color ? Brushes.LightGray : Brushes.Silver;

            // Draw a Flag
            if (Show_Flag)
            {
                e.Graphics.FillRectangle(Standard_Brush, Rect); 
                DrawFlag(e.Graphics);
                return;
            }

            // Draw an "untouched" rectangle 
            if (!Show_Content) 
            { 
                e.Graphics.FillRectangle(Standard_Brush, Rect);                  
                return;
            }

            // Draw a mine
            if (Is_A_Mine)
            {
                // If this cell is a mine boom, change background
                Clicked_Brush = Is_Mine_Boom ? Brushes.Yellow : Clicked_Brush;

                e.Graphics.FillRectangle(Clicked_Brush, Rect);
                DrawMine(e.Graphics);
                return;
            }
          
            // Draw an empty Rect or a number
            e.Graphics.FillRectangle(Clicked_Brush, Rect);

            if (Nearby_Mines_Count == 0)
                return;

            e.Graphics.DrawString(Nearby_Mines_Count.ToString(), Current_Font, Color_Map[Nearby_Mines_Count], Rect, Current_Format);            
        }

        public void MouseEvent(MouseEventArgs e)
        {

            // If mouse is outside the cell, don't handle this event
            if (!Rect.Contains(e.Location))
                return;

            // If show the content, don't handle this event
            if (Show_Content)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:

                    // If there is a flag , don't handle this event
                    if (Show_Flag )
                        return;

                    // Notify the Game Box that the user hit a mine or hit an empty cell
                    var Event_Type = Is_A_Mine ? Event.Mine_Boom : Event.Empty_Cell;

                    // Change background if an user click on this mine cell
                    Is_Mine_Boom = (Event_Type == Event.Mine_Boom);

                    Mediator?.Notify(this, Event_Type);

                    break;
                case MouseButtons.Right:

                    // Show / Hide a Flag
                    Show_Flag = !Show_Flag;

                    Mediator?.Notify(this, Event.Update_Flags_Count);

                    break;
            }    
        }

        public void Dispose()
        {
            Current_Font.Dispose();
            Current_Format.Dispose();
            Color_Map.Clear();
        }
    }
}

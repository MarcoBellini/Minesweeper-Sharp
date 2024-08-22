﻿
namespace Minesweeper_Sharp.Engine
{ 
    public class GameCounter : BaseComponent, IGameObject, IDisposable
    {
        private Rectangle Counter_Rect;
        private Image? Counter_Image;
        private int Counter_Image_Size;

        // Font and format for Text function
        private readonly Font Current_Font = new Font("Segoe UI", 14, FontStyle.Bold);
        private StringFormat Current_Format = new();

        /// <summary>
        /// Create new Counter
        /// </summary>
        /// <param name="Rect">Position</param>
        /// <param name="Img">Left-side image</param>
        /// <param name="Image_Size">Image size</param>
        public GameCounter(Rectangle Rect, Image? Img, int Image_Size = 32) : base(null)
        {
            Counter_Rect = Rect;
            Counter_Image = Img;
            Counter_Image_Size = Image_Size;
            Counter_Value = 0;

            // Center text to a Rectangle
            Current_Format.LineAlignment = StringAlignment.Center;
            Current_Format.Alignment = StringAlignment.Center;
        }

        /// <summary>
        /// Set a new value for the counter
        /// </summary>
        public int Counter_Value { get; set; }


        /// <summary>
        /// Get Rectangle of the Counter
        /// </summary>
        public Rectangle Counter_Rectangle { get => Counter_Rect; }


        public void Draw(PaintEventArgs e)
        {
            var String_Rect = new Rectangle(Counter_Rect.X, Counter_Rect.Y, Counter_Rect.Width, Counter_Rect.Height);

            if (Counter_Image != null)      
            {
                var Image_Rect = new Rectangle(Counter_Rect.X, Counter_Rect.Y, Counter_Image_Size, Counter_Image_Size);

                e.Graphics.DrawImage(Counter_Image, Image_Rect);

                // Adjust location to fit the string
                String_Rect.Offset(Counter_Image_Size, 0);
                String_Rect.Width -= Counter_Image_Size;
            }    
                              
            e.Graphics.DrawString(Counter_Value.ToString(), Current_Font, Brushes.Black, String_Rect, Current_Format);          
        }

        public void MouseEvent(MouseEventArgs e)
        {
            // Nothing to do here..
        }
        public void Dispose()
        {
            Current_Font.Dispose();
            Current_Format.Dispose();
            Counter_Image = null;
        }


    }
}

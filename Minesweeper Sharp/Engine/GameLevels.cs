
namespace Minesweeper_Sharp.Engine
{

    /// <summary>
    /// Available levels
    /// </summary>
    public enum Game_Level
    {
        Beginner,
        Intermediate,
        Expert
    };

    /// <summary>
    /// Used to store levels informations
    /// </summary>
    public class GameLevel
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Mines { get; set; }

        public int Cell_Size { get; set; }

        public Size Form_Size { get; set; }

        public Rectangle Flag_Rect { get; set; }

        public Rectangle Crono_Rect { get; set; }

        public Rectangle Banner_Rect { get; set; }

    }

    /// <summary>
    /// Maintains pre-packaged levels
    /// </summary>
    public static class GameLevels 
    {
        public const int Menubar_Top_Padding = 35; // Height of menu in main form in px
        public const int Counter_Image_Size = 25; // Height of Counters images in px
        public const int Grid_Top_Padding = 10 + Menubar_Top_Padding + Counter_Image_Size; // Top padding for game grid in px


        public static readonly GameLevel Beginner_Level = new() 
        {
            Rows = 9,
            Columns = 9,
            Mines = 10,
            Cell_Size = 30,
            Form_Size = new Size(286, 309 + Grid_Top_Padding),
            Flag_Rect = new(55, Menubar_Top_Padding, Counter_Image_Size + 40, Counter_Image_Size),
            Crono_Rect = new(125 + Counter_Image_Size, Menubar_Top_Padding, Counter_Image_Size + 40, Counter_Image_Size),
            Banner_Rect = new(2, Menubar_Top_Padding + 2, 60, 30)
        };

        public static readonly GameLevel Intermediate_Level = new()
        {    
            Rows = 16,
            Columns = 16,
            Mines = 40,
            Cell_Size = 25,
            Form_Size = new Size(416, 439 + Grid_Top_Padding),
            Flag_Rect = new(110, Menubar_Top_Padding, Counter_Image_Size + 40, Counter_Image_Size),
            Crono_Rect = new(190 + Counter_Image_Size, Menubar_Top_Padding, Counter_Image_Size + 40, Counter_Image_Size),
            Banner_Rect = new(20, Menubar_Top_Padding + 2, 60, 30)
        };

        public static readonly GameLevel Expert_Level = new()
        {
            Rows = 16,
            Columns = 30,
            Mines = 99,
            Cell_Size = 20,
            Form_Size = new Size(616, 360 + Grid_Top_Padding),
            Flag_Rect = new(200, Menubar_Top_Padding, Counter_Image_Size + 40, Counter_Image_Size),
            Crono_Rect = new(300 + Counter_Image_Size, Menubar_Top_Padding, Counter_Image_Size + 40, Counter_Image_Size),
            Banner_Rect = new(30, Menubar_Top_Padding + 2, 50, 30)
        };
    }
    
}

using System.Reflection;
using System.Timers;
using Timer = System.Timers.Timer;

/*
 
Icons in this programs are used
 
 Flag icon attribution
 <a href="https://www.flaticon.com/free-icons/Flag_Image" title="Flag_Image icons">Flag icons created by Smashicons - Flaticon</a>

 Timer icon Attribution
<a href="https://www.flaticon.com/free-icons/time" title="time icons">Time icons created by Freepik - Flaticon</a>

 Application Icon Attribution
<a href="https://www.flaticon.com/free-icons/bomb" title="bomb icons">Bomb icons created by surang - Flaticon</a>
 */

namespace Minesweeper_Sharp.Engine
{
    /// <summary>
    /// Manage all the Game-Logic. Create Cells, Counters a Banner.
    /// Coordinates all objects on mediator model mixed with Composite pattern
    /// </summary>
    public class GameManager : IGameObject, IMediator, IDisposable
    {
        private Form Game_Form;
        private GameCellsManager CellsManager;
        private GameCounter FlagsCounter;
        private GameCounter CronoCounter;
        private GameBanner GameBanner;

        private bool Is_First_Click;
        private bool Is_Game_Win_Or_Loose;
        private Timer Game_Timer;


        public GameManager(Game_Level Level, Form Frm) 
        {            
            GameLevel Params = GameLevels.Beginner_Level;

            switch (Level) 
            {
                case Game_Level.Beginner:
                    Params = GameLevels.Beginner_Level;
                    break;
                case Game_Level.Intermediate:
                    Params = GameLevels.Intermediate_Level;
                    break;
                case Game_Level.Expert:
                    Params = GameLevels.Expert_Level;
                    break;
            }

            Game_Form = Frm;
            Frm.Size = Params.Form_Size;
            
            // Loading images for Counters
            var assembly = Assembly.GetExecutingAssembly();
            var Flag_Stream = assembly.GetManifestResourceStream("Minesweeper_Sharp.Icons.flag.png");
            var Crono_Stream = assembly.GetManifestResourceStream("Minesweeper_Sharp.Icons.chronometer.png");

            var Flag_Image = Flag_Stream is null ? null : Image.FromStream(Flag_Stream);
            var Crono_Image = Crono_Stream is null ? null : Image.FromStream(Crono_Stream);


            CellsManager = new(Params.Rows, Params.Columns, Params.Mines, GameLevels.Grid_Top_Padding, Params.Cell_Size);
            FlagsCounter = new(Params.Flag_Rect, Flag_Image, GameLevels.Counter_Image_Size);
            CronoCounter = new(Params.Crono_Rect, Crono_Image, GameLevels.Counter_Image_Size);
            GameBanner = new(Params.Banner_Rect);

            FlagsCounter.Counter_Value = Params.Mines;
            Is_First_Click = true;
            Is_Game_Win_Or_Loose = false;

            Game_Timer = new(1000);

            CellsManager.SetMediator(this);
        }

        public void Draw(PaintEventArgs e)
        {    
            // Draw only requied items

            if(e.ClipRectangle.IntersectsWith(FlagsCounter.Counter_Rectangle))
                FlagsCounter.Draw(e);

            if (e.ClipRectangle.IntersectsWith(CronoCounter.Counter_Rectangle))
                CronoCounter.Draw(e);

            if (e.ClipRectangle.IntersectsWith(CellsManager.Game_Cells_Rect))
                CellsManager.Draw(e);

            if (e.ClipRectangle.IntersectsWith(GameBanner.Banner_Rectangle))
                GameBanner.Draw(e);
        }

        public void MouseEvent(MouseEventArgs e)
        {
            if (Is_Game_Win_Or_Loose)
                return;

            if ((Is_First_Click) && (e.Button == MouseButtons.Right))
                return;

            if (Is_First_Click)
            {               
                CellsManager.Create_Game(e.Location);
                Game_Timer.Enabled = true;
                Game_Timer.Elapsed += OnTimedEvent;
                Game_Timer.AutoReset = true;                
                Is_First_Click = false;
            }               

            CellsManager.MouseEvent(e);

            // Update only cells area
            Game_Form.Invalidate(CellsManager.Game_Cells_Rect); 
        }

        public void Notify(object sender, Event ev)
        {
            switch (ev)
            {
                case Event.Update_Flags_Count:
                    var m = (GameCellsManager)sender;
                    FlagsCounter.Counter_Value = m.Mines_Count - m.Flags_Count;
                    Game_Form.Invalidate(FlagsCounter.Counter_Rectangle);
                    break;
                case Event.Loose_Game:                    
                case Event.Win_Game:
                    GameBanner.Banner_Text = (ev == Event.Win_Game) ? "Win!" : "Oops";
                    GameBanner.Banner_Brush = (ev == Event.Win_Game) ? Brushes.Green : Brushes.Red;

                    GameBanner.Banner_Visible = true;   
                    Is_Game_Win_Or_Loose = true;
                    Game_Timer.Enabled= false;

                    Game_Form.Invalidate(GameBanner.Banner_Rectangle);
                    break;
            }
        }

        public void Dispose()
        {
            Game_Timer.Dispose();
            CellsManager.Dispose();
            CronoCounter.Dispose();
            FlagsCounter.Dispose();            
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            // Invalidate only the Crono Counter
            CronoCounter.Counter_Value++;
            Game_Form.Invalidate(CronoCounter.Counter_Rectangle);  
        }
    }
}

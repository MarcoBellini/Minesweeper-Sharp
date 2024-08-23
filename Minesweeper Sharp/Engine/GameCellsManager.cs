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

namespace Minesweeper_Sharp.Engine
{
    public class GameCellsManager : BaseComponent, IGameObject, IMediator, IDisposable
    {
        private readonly List<GameCell> Cells_List = new List<GameCell>();
        private readonly int Rows;
        private readonly int Columns;
        private readonly int Mines;       
        
        /// <summary>
        /// Prepare cells without mines. 
        /// </summary>
        /// <param name="Rows">Number of rows</param>
        /// <param name="Columns">Number of Columns</param>
        /// <param name="Mines">Number of Mines</param>
        /// <param name="Top_Padding">Padding from top in px where the first row begin</param>
        /// <param name="Cell_Size">GameCell size in px (square cell)</param>
        /// <exception cref="ArgumentException"></exception>
        public GameCellsManager(int Rows, int Columns, int Mines, int Top_Padding = 0, int Cell_Size = 20) : base(null)
        {            
            if (Rows < 8 || Columns < 8) throw new ArgumentException("Rows and Columns must be higher than 7");
            if (Rows > 30 || Columns > 30) throw new ArgumentException("Rows and Columns must be lower than 31");
            if (Mines < 8 || Mines > 99) throw new ArgumentException("Mines must be lower than 99 and higher than 7");

            this.Rows = Rows;
            this.Columns = Columns;
            this.Mines = Mines;

            var Cell_Index = 0;
            var Is_Even_Color = false;
            var Is_Row_Beginning_Color_Even = false;

            // Create a list of cells with an alternate color pattern
            for (int i = 0; i < Rows; i++)
            {
                Is_Row_Beginning_Color_Even = Is_Even_Color;

                for (int j = 0; j < Columns; j++)
                {
                    Cells_List.Add(new GameCell(Cell_Index, new Rectangle(j * Cell_Size, Top_Padding + i * Cell_Size, Cell_Size, Cell_Size), Is_Even_Color));
                    Cell_Index++;
                    Is_Even_Color = !Is_Even_Color;
                }

                if (Is_Even_Color == Is_Row_Beginning_Color_Even)
                    Is_Even_Color = !Is_Even_Color;

            }

            // Store cells area (used later during painting)
            Game_Cells_Rect = new(0, Top_Padding, Columns * Cell_Size, Rows * Cell_Size);

            Cells_List.ForEach(Cell => Cell.SetMediator(this));
        }

        /// <summary>
        /// Get current cells rect
        /// </summary>
        public Rectangle Game_Cells_Rect { get; private set; }

        /// <summary>
        /// Prepare mines and calculate numbers near the mines to help
        /// the user to solve the game.
        /// </summary>
        public void Create_Game(Point First_Click)
        {
            Random Rand = new Random();
            List<int> Rand_Numbers = new List<int>(Mines);

            var Mine_Index = 0;
            var Max_Value = Rows * Columns - 1;

            // Create a random array (random mines in the game)
            while(Mine_Index < Mines)
            {
                var Rand_Number = Rand.Next(0, Max_Value);  
                
                if(!Rand_Numbers.Contains(Rand_Number))
                {
                    // First Click must be always valid
                    if (!Cells_List[Rand_Number].Cell_Rect.Contains(First_Click))
                    {
                        Rand_Numbers.Add(Rand_Number);
                        Mine_Index++;
                    }
                }
            }

            // Calculate numbers of adjacent Items of a mine
            foreach (var Item in Rand_Numbers)
            {
                Cells_List[Item].Is_A_Mine = true;

                Adjacent_Elements(Cells_List, Item, Rows, Columns)
                    .ToList()
                    .ForEach(i => i.Nearby_Mines_Count++);
            }
            
        }

        /// <summary>
        /// Find adjacents elements in a 2D array.
        /// </summary>
        /// <returns>Returns a set of adjacent elements</returns>
        private static IEnumerable<T> Adjacent_Elements<T>(List<T> Cells, int Item_Index, int Rows, int Columns)
        {
            var Column_Index = Item_Index % Columns;
            var Row_Index = Item_Index / Columns;

            for (var i = Row_Index - 1; i <= Row_Index + 1; i++)            
                for (var j = Column_Index - 1; j <= Column_Index + 1; j++)              
                    if (i >= 0 && j >= 0 && i < Rows && j < Columns && !(i == Row_Index && j == Column_Index))
                        yield return Cells[i * Columns + j];               

        }

        /// <summary>
        /// From an empty Item as input, reveal adjacent empty cells and first row or column that is a number near a mine
        /// </summary>
        /// <param name="Index">Index of the empty Item</param>
        private void Reveal_Nearest_Cells(int Index)
        {
            var Cells = Adjacent_Elements(Cells_List, Index, Rows, Columns);           
                      
            foreach(var Item in Cells)
            {
                // If we have an empty Item, reveal the content and find
                // others nearest empty cells and check recursively adjacents cells. (skip flags)
                if ((Item.Nearby_Mines_Count == 0) && !Item.Is_A_Mine && !Item.Show_Content && !Item.Show_Flag)
                {
                    Item.Show_Content = true;
                    Reveal_Nearest_Cells(Item.Index);                    
                }

               // If we reach this point, we only have a number to reveal  (skip flags)
               if(!Item.Show_Flag)
                Item.Show_Content = true;
            }
        }

        /// <summary>
        /// Get the number of mines
        /// </summary>
        public int Mines_Count
        {
            get => Mines;
        }

        /// <summary>
        /// Get the number of cells marked with a flag
        /// </summary>
        public int Flags_Count 
        {
            get => Cells_List.Where(i => i.Show_Flag).Count();  
        }


        public void Dispose()
        {
            Cells_List.ForEach(cell => cell.Dispose());
            Cells_List.Clear();
        }

        public void Draw(PaintEventArgs e)
        {
            Cells_List.ForEach(cell => cell.Draw(e));
        }

        public void MouseEvent(MouseEventArgs e)
        {
            Cells_List.ForEach((cell) => cell.MouseEvent(e));    
        }

        public void Notify(object sender, Event ev)
        {
            GameCell Item = (GameCell)sender;

            switch (ev)
            {
                case Event.Empty_Cell:

                    // If the Item is empty, reveals the cells up to the first number in the row or column
                    // otherwise show the content
                    if (Item.Nearby_Mines_Count == 0)
                        Reveal_Nearest_Cells(Item.Index);

                    Item.Show_Content = true;

                    // If there are no empty cells, notify a WIN
                    if (Cells_List.Where(i => !i.Show_Content && !i.Is_A_Mine).Count() == 0)
                    {
                        // Mark with a flag every mine leave alone without a flag, update the counter 
                        // and then signals the win
                        Cells_List.Where(i => !i.Show_Content && i.Is_A_Mine).ToList().ForEach(v => v.Show_Flag = true);
                        Mediator?.Notify(this, Event.Update_Flags_Count);

                        Mediator?.Notify(this, Event.Win_Game);
                    }

                       

                    break;
                case Event.Mine_Boom:

                    // Show the content of a cell that is a mine and reveal wrong flags
                    Cells_List.Where(i => i.Is_A_Mine || i.Show_Flag).ToList().ForEach(i => i.Show_Content = true);
                    Mediator?.Notify(this, Event.Loose_Game);

                    break;

                case Event.Update_Flags_Count:
                    Mediator?.Notify(this, Event.Update_Flags_Count);
                    break;
            }
        }        
    }
}

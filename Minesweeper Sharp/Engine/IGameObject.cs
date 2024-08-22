
namespace Minesweeper_Sharp.Engine
{
    /// <summary>
    /// Use this interafce to implement a Composite strucural pattern
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Draw an object using an existing graphics object
        /// </summary>
        /// <param name="e">Graphics object</param>
        public void Draw(PaintEventArgs e);

        /// <summary>
        /// Handle a MouseEvent event
        /// </summary>
        /// <param name="e">MouseEventArgs argument</param>
        public void MouseEvent(MouseEventArgs e);

    }
}

﻿/*
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

using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class AbstractCellDrawable : Drawable
    {
        /// <summary>
        /// Color to fill the object.
        /// </summary>
        protected Color HighlightedColor => _highlightedColor;

        private Color _highlightedColor;

        /// <summary>
        /// Model which is drawn.
        /// </summary>
        protected AbstractCell _cell;

        /// <summary>
        /// Rectangle for any effects, applied to effects.
        /// </summary>
        protected RectangleShape _effectRectangle;

        /// <summary>
        /// The flag detecting whether the cell is visible on screen.
        /// </summary>
        protected bool _visible;

        /// <summary>
        /// Init new abstract cell which will be drawn on the screen.
        /// </summary>
        /// <param name="cell">Model of the cell</param>
        /// <param name="cellSize">Dimensions of the cell on screen</param>
        /// <param name="highlightedColor">Color to fill the cell</param>
        public AbstractCellDrawable(AbstractCell cell, int cellSize, Color highlightedColor)
        {
            _highlightedColor = highlightedColor;
            _cell             = cell;
            _effectRectangle = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = HighlightedColor
            };
        }

        public void SetPosition(int x, int y)
        {
            _effectRectangle.Position = new Vector2f(x, y);
        }

        public void ShiftPosition(int x, int y)
        {
            var shiftVector = new Vector2f(x, y);
            _effectRectangle.Position += shiftVector;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_cell.Dirty)
            {
                Update();
                _cell.ClearDirty();
            }

            if (!_visible) return;
            target.Draw(_effectRectangle);
        }

        private void Update()
        {
            _visible = _cell.Visible;
        }
    }
}
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class PathCellDrawable : Drawable
    {
        private static readonly Color HighlightedColor = new Color(255, 198, 41, 150);

        private PathCell _pathCell;

        /// <summary>
        /// Rectangle for any effects, applied to effects
        /// </summary>
        private RectangleShape _effectRectangle;

        private bool _visible;

        public PathCellDrawable(PathCell pathCell, int cellSize)
        {
            _pathCell = pathCell;
            _effectRectangle = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = HighlightedColor
            };
        }

        public void ShiftPosition(int x, int y)
        {
            var shiftVector = new Vector2f(x, y);
            _effectRectangle.Position += shiftVector;
        }

        public void SetPosition(int x, int y)
        {
            _effectRectangle.Position = new Vector2f(x, y);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_pathCell.Dirty)
            {
                Update();
                _pathCell.ClearDirty();
            }

            if (!_visible) return;
            target.Draw(_effectRectangle);
        }

        private void Update()
        {
            _visible = _pathCell.Visible;
        }
    }
}
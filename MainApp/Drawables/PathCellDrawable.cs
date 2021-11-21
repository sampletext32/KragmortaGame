using MainApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Drawables
{
    public class PathCellDrawable : Drawable
    {
        private static readonly Color HighlightedColor = new Color(255, 198, 41, 150);

        private PathCell _pathCell;

        /// <summary>
        /// Rectangle for any effects, applied to effects
        /// </summary>
        private RectangleShape _effectRectangle;

        public PathCellDrawable(PathCell pathCell, int cellSize)
        {
            _pathCell = pathCell;
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

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_pathCell.Dirty)
            {
                Update();
                _pathCell.ClearDirty();
            }

            target.Draw(_effectRectangle);
        }

        private void Update()
        {
        }
    }
}
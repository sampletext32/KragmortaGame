using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class PushCellDrawable : Drawable
    {
        private static readonly Color HighlightedColor = new Color(255, 192, 203, 150);

        private PushCell _pushCell;
        
        /// <summary>
        /// Rectangle for any effects, applied to effects
        /// </summary>
        private RectangleShape _effectRectangle;

        private bool _visible;
        
        public PushCellDrawable(PushCell pushCell, int cellSize)
        {
            _pushCell = pushCell;
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
            if (_pushCell.Dirty)
            {
                Update();
                _pushCell.ClearDirty();
            }

            if (!_visible) return;
            target.Draw(_effectRectangle);
        }

        private void Update()
        {
            _visible = _pushCell.Visible;
        }
    }
}
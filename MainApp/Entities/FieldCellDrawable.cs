using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities
{
    public class FieldCellDrawable : Drawable
    {
        /// <summary>
        /// Background rectangle
        /// </summary>
        private RectangleShape _cell;

        /// <summary>
        /// Red sub-rect
        /// </summary>
        private RectangleShape _red;

        /// <summary>
        /// Green sub-rect
        /// </summary>
        private RectangleShape _green;

        /// <summary>
        /// Blue sub-rect
        /// </summary>
        private RectangleShape _blue;


        // Visibility flags

        private bool _isRedVisible;
        private bool _isGreenVisible;
        private bool _isBlueVisible;

        private float _outlineThickness;

        public void SetFlagsVisibility(FieldType type)
        {
            _isRedVisible   = (type & FieldType.Red) == FieldType.Red;
            _isGreenVisible = (type & FieldType.Green) == FieldType.Green;
            _isBlueVisible  = (type & FieldType.Blue) == FieldType.Blue;
        }

        public void SetClicked(bool selected)
        {
            if (selected)
            {
                _cell.FillColor = new Color(127, 127, 127);
            }
            else
            {
                _cell.FillColor = Color.White;
            }
        }

        public void SetPosition(int x, int y)
        {
            _cell.Position  = new Vector2f(x, y);
            _red.Position   = new Vector2f(x + 10, y + 10);
            _green.Position = new Vector2f(x + 30, y + 10);
            _blue.Position  = new Vector2f(x + 50, y + 10);
        }

        public void SetOutlineThickness(float thickness)
        {
            _outlineThickness = thickness;
        }

        public void SetOutlineVisible(bool visible)
        {
            if (visible)
            {
                _cell.OutlineThickness = _outlineThickness;
                _cell.OutlineColor     = Color.Magenta;
            }
            else
            {
                _cell.OutlineThickness = 0;
                _cell.OutlineColor     = Color.Transparent;
            }
        }

        public FieldCellDrawable(int cellSize)
        {
            _cell = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = Color.White
            };
            _red = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Red
            };
            _green = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Green
            };
            _blue = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Blue
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_cell);
            if (_isRedVisible) target.Draw(_red);
            if (_isGreenVisible) target.Draw(_green);
            if (_isBlueVisible) target.Draw(_blue);
        }
    }
}
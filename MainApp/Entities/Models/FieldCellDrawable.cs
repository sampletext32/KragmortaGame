using MainApp.Entities.Enums;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities.Models
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

        /// <summary>
        /// Orange sub-rect
        /// </summary>
        private RectangleShape _orange;


        // Visibility flags

        private bool _isRedVisible;
        private bool _isGreenVisible;
        private bool _isBlueVisible;
        private bool _isOrangeVisible;

        private float _outlineThickness;

        public void SetFlagsVisibility(CellType type)
        {
            _isRedVisible    = (type & CellType.Red) == CellType.Red;
            _isGreenVisible  = (type & CellType.Green) == CellType.Green;
            _isBlueVisible   = (type & CellType.Blue) == CellType.Blue;
            _isOrangeVisible = (type & CellType.Orange) == CellType.Orange;
        }

        public void SetClicked(bool selected)
        {
            if (selected)
            {
                _cell.FillColor = new Color(127, 127, 127);
            }
            else
            {
                _cell.FillColor = new Color(50, 50, 50, 255);
            }
        }

        public void SetPosition(int x, int y)
        {
            _cell.Position   = new Vector2f(x, y);
            _red.Position    = new Vector2f(x + 10, y + 10);
            _green.Position  = new Vector2f(x + 30, y + 10);
            _blue.Position   = new Vector2f(x + 50, y + 10);
            _orange.Position = new Vector2f(x + 70, y + 10);
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
                FillColor = new Color(50, 50, 50, 255)
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
            _orange = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = new Color(255, 165, 0)
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_cell);
            if (_isRedVisible) target.Draw(_red);
            if (_isGreenVisible) target.Draw(_green);
            if (_isBlueVisible) target.Draw(_blue);
            if (_isOrangeVisible) target.Draw(_orange);
        }
    }
}
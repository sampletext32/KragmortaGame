using MainApp.Entities;
using MainApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Models
{
    public class FieldCellDrawable : Drawable
    {
        private readonly FieldCell _cell;

        /// <summary>
        /// Background rectangle
        /// </summary>
        private RectangleShape _backgroundRectangle;

        /// <summary>
        /// Rectangle for any effects, applied to effects
        /// </summary>
        private RectangleShape _effectRectangle;

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


        private static readonly Color DefaultBackgroundColor = new Color(50, 50, 50, 255);
        private static readonly Color DefaultOutlineColor = Color.Magenta;
        private static readonly Color ClickedColor = new Color(127, 127, 127);
        private static readonly Color HighlightedColor = new Color(255, 198, 41, 150);

        public FieldCellDrawable(FieldCell cell, int cellSize)
        {
            _cell = cell;
            _backgroundRectangle = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = DefaultBackgroundColor
            };
            _effectRectangle = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = Color.Transparent
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

        public void SetPosition(int x, int y)
        {
            _backgroundRectangle.Position = new Vector2f(x, y);
            _effectRectangle.Position     = new Vector2f(x, y);
            _red.Position                 = new Vector2f(x + 10, y + 10);
            _green.Position               = new Vector2f(x + 30, y + 10);
            _blue.Position                = new Vector2f(x + 50, y + 10);
            _orange.Position              = new Vector2f(x + 70, y + 10);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_backgroundRectangle);
            if (_isRedVisible) target.Draw(_red);
            if (_isGreenVisible) target.Draw(_green);
            if (_isBlueVisible) target.Draw(_blue);
            if (_isOrangeVisible) target.Draw(_orange);
            target.Draw(_effectRectangle);
        }

        public void Update()
        {
            SetOutlineVisible(_cell.Hovered);
            SetFlagsVisibility(_cell.Type);
            SetClicked(_cell.Clicked);
            SetHighlighted(_cell.Highlighted);
        }

        public void SetOutlineThickness(float thickness)
        {
            _outlineThickness = thickness;
        }

        private void SetOutlineVisible(bool visible)
        {
            if (visible)
            {
                _backgroundRectangle.OutlineThickness = _outlineThickness;
                _backgroundRectangle.OutlineColor     = DefaultOutlineColor;
            }
            else
            {
                _backgroundRectangle.OutlineThickness = 0;
                _backgroundRectangle.OutlineColor     = Color.Transparent;
            }
        }

        private void SetFlagsVisibility(CellType type)
        {
            _isRedVisible    = (type & CellType.Red) == CellType.Red;
            _isGreenVisible  = (type & CellType.Green) == CellType.Green;
            _isBlueVisible   = (type & CellType.Blue) == CellType.Blue;
            _isOrangeVisible = (type & CellType.Orange) == CellType.Orange;
        }


        private void SetClicked(bool selected)
        {
            if (selected)
            {
                _backgroundRectangle.FillColor = ClickedColor;
            }
            else
            {
                _backgroundRectangle.FillColor = DefaultBackgroundColor;
            }
        }

        private void SetHighlighted(bool highlighted)
        {
            if (highlighted)
            {
                _effectRectangle.FillColor = HighlightedColor;
            }
            else
            {
                _effectRectangle.FillColor = Color.Transparent;
            }
        }
    }
}
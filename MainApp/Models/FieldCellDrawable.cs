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
        ///     Background rectangle
        /// </summary>
        private readonly RectangleShape _backgroundRectangle;

        /// <summary>
        ///     Red sub-rect
        /// </summary>
        private readonly RectangleShape _red;

        /// <summary>
        ///     Green sub-rect
        /// </summary>
        private readonly RectangleShape _green;

        /// <summary>
        ///     Blue sub-rect
        /// </summary>
        private readonly RectangleShape _blue;

        /// <summary>
        ///     Orange sub-rect
        /// </summary>
        private readonly RectangleShape _orange;

        private readonly RectangleShape _moveTargetRectangle;

        // Visibility flags
        private bool _isMoveTargetVisible;
        private bool _isRedVisible;
        private bool _isBlueVisible;
        private bool _isGreenVisible;
        private bool _isOrangeVisible;

        private float _outlineThickness;

        private static readonly Color DefaultColor = new Color(50, 50, 50, 255);
        private static readonly Color MoveTargetColor = new Color(255, 255, 0, 100);
        private static readonly Color ClickedColor = new Color(127, 127, 127);


        public FieldCellDrawable(FieldCell cell, int cellSize, int cellMargin)
        {
            _cell = cell;
            SetOutlineThickness(cellMargin);
            _backgroundRectangle = new RectangleShape
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = DefaultColor
            };
            _red = new RectangleShape
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Red
            };
            _green = new RectangleShape
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Green
            };
            _blue = new RectangleShape
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Blue
            };
            _orange = new RectangleShape
            {
                Size      = new Vector2f(10, 10),
                FillColor = new Color(255, 165, 0)
            };
            _moveTargetRectangle = new RectangleShape()
            {
                Size      = new Vector2f(cellSize - 10, cellSize - 10),
                FillColor = MoveTargetColor
            };
        }

        public void SetPosition(int x, int y)
        {
            _backgroundRectangle.Position = new Vector2f(x, y);
            _moveTargetRectangle.Position = new Vector2f(x + 5, y + 5);
            _red.Position                 = new Vector2f(x + 10, y + 10);
            _green.Position               = new Vector2f(x + 30, y + 10);
            _blue.Position                = new Vector2f(x + 50, y + 10);
            _orange.Position              = new Vector2f(x + 70, y + 10);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_backgroundRectangle);
            if (_isMoveTargetVisible) target.Draw(_moveTargetRectangle);
            if (_isRedVisible) target.Draw(_red);
            if (_isGreenVisible) target.Draw(_green);
            if (_isBlueVisible) target.Draw(_blue);
            if (_isOrangeVisible) target.Draw(_orange);
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
                _backgroundRectangle.FillColor = DefaultColor;
        }

        private void SetOutlineThickness(float thickness)
        {
            _outlineThickness = thickness;
        }

        private void SetOutlineVisible(bool visible)
        {
            if (visible)
            {
                _backgroundRectangle.OutlineThickness = _outlineThickness;
                _backgroundRectangle.OutlineColor     = Color.Magenta;
            }
            else
            {
                _backgroundRectangle.OutlineThickness = 0;
                _backgroundRectangle.OutlineColor     = Color.Transparent;
            }
        }

        private void SetIsMoveTargetVisible(bool visible)
        {
            _isMoveTargetVisible = visible;
        }

        public void Update()
        {
            SetOutlineVisible(_cell.Hovered);
            SetFlagsVisibility(_cell.Type);
            SetClicked(_cell.Clicked);
            SetIsMoveTargetVisible(_cell.IsPossibleMoveTarget);
        }
    }
}
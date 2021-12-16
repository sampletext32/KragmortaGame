using System;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables.FieldCellDrawables
{
    public class TexturedFieldCellDrawable : Drawable
    {
        public AbstractCell Cell => _cell;

        protected readonly FieldCell _cell;

        /// <summary>
        /// Background rectangle
        /// </summary>
        protected Sprite _backgroundSprite;

        protected Image _backgroundImage;

        protected Sprite _hoveredEffectRectangle;

        /// <summary>
        /// Red sub-rect
        /// </summary>
        protected RectangleShape _red;

        /// <summary>
        /// Green sub-rect
        /// </summary>
        protected RectangleShape _green;

        /// <summary>
        /// Blue sub-rect
        /// </summary>
        protected RectangleShape _blue;

        /// <summary>
        /// Orange sub-rect
        /// </summary>
        protected RectangleShape _orange;


        // Visibility flags

        protected bool _isRedVisible;
        protected bool _isGreenVisible;
        protected bool _isBlueVisible;
        protected bool _isOrangeVisible;
        protected bool _isHoveredVisible;

        protected static readonly Color DefaultBackgroundColor = new Color(255, 255, 255, 255);
        protected static readonly Color ClickedColor = new Color(127, 127, 127);
        protected static readonly Color HoveredColor = new Color(255, 255, 255, 50);

        protected static Random random = new Random(DateTime.Now.Millisecond);

        public TexturedFieldCellDrawable(FieldCell cell, int cellSize)
        {
            _cell = cell;
        }

        public void SetPosition(int x, int y)
        {
            _backgroundSprite.Position       = new Vector2f(x, y);
            _hoveredEffectRectangle.Position = new Vector2f(x, y);
            _red.Position                    = new Vector2f(x + 10, y + 10);
            _green.Position                  = new Vector2f(x + 30, y + 10);
            _blue.Position                   = new Vector2f(x + 50, y + 10);
            _orange.Position                 = new Vector2f(x + 70, y + 10);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_cell.Dirty)
            {
                Update();
                _cell.ClearDirty();
            }

            target.Draw(_backgroundSprite);
            if (_isRedVisible) target.Draw(_red);
            if (_isGreenVisible) target.Draw(_green);
            if (_isBlueVisible) target.Draw(_blue);
            if (_isOrangeVisible) target.Draw(_orange);
            if (_isHoveredVisible) target.Draw(_hoveredEffectRectangle);
        }

        protected void Update()
        {
            SetHoveredVisible(_cell.Hovered);
            SetFlagsVisibility(_cell.Type);
            SetClicked(_cell.Clicked);
        }

        protected void SetHoveredVisible(bool visible)
        {
            _isHoveredVisible = visible;
        }

        protected void SetFlagsVisibility(CellType type)
        {
            _isRedVisible    = (type & CellType.Red) == CellType.Red;
            _isGreenVisible  = (type & CellType.Green) == CellType.Green;
            _isBlueVisible   = (type & CellType.Blue) == CellType.Blue;
            _isOrangeVisible = (type & CellType.Orange) == CellType.Orange;
        }


        protected void SetClicked(bool selected)
        {
            if (selected)
            {
                _backgroundSprite.Color = ClickedColor;
            }
            else
            {
                _backgroundSprite.Color = DefaultBackgroundColor;
            }
        }

        public bool IsMouseWithinBounds(int x, int y)
        {
            return !(x < _backgroundSprite.GetGlobalBounds().Left) && !(x > _backgroundSprite.GetGlobalBounds().Left +
                _backgroundSprite.GetGlobalBounds().Width) && !(y < _backgroundSprite.GetGlobalBounds().Top) && !(y > _backgroundSprite.GetGlobalBounds().Top + _backgroundSprite.GetGlobalBounds().Height);

            // Console.WriteLine(_backgroundImage.GetPixel(
            //     (uint)(x - (int)_backgroundSprite.GetGlobalBounds().Left),
            //     (uint)(y - (int)_backgroundSprite.GetGlobalBounds().Top)));
            //
            // return _backgroundImage.GetPixel(
            //     (uint)(x - (int)_backgroundSprite.GetGlobalBounds().Left),
            //     (uint)(y - (int)_backgroundSprite.GetGlobalBounds().Top)) != Color.Transparent;
        }

        public bool IsTransparentPixel(int x, int y)
        {
            var textureX = (uint)(x - (int)_backgroundSprite.GetGlobalBounds().Left);
            var textureY = (uint)(y - (int)_backgroundSprite.GetGlobalBounds().Top);

            textureX = (uint)(textureX / _backgroundSprite.Scale.X);
            textureY = (uint)(textureY / _backgroundSprite.Scale.X);
            var pixel = _backgroundImage.GetPixel(
                textureX,
                textureY);
            Console.WriteLine($"TEXTURE ({textureX}, {textureY}): {pixel}");

            return pixel.A == 0;
        }
    }
}
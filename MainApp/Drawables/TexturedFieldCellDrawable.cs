using System;
using MainApp.Entities;
using MainApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Drawables
{
    public class TexturedFieldCellDrawable : Drawable
    {
        private readonly FieldCell _cell;

        /// <summary>
        /// Background rectangle
        /// </summary>
        private Sprite _backgroundSprite;

        private RectangleShape _hoveredEffectRectangle;

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
        private bool _isHoveredVisible;

        private static readonly Color DefaultBackgroundColor = new Color(255, 255, 255, 255);
        private static readonly Color ClickedColor = new Color(127, 127, 127);
        private static readonly Color HoveredColor = new Color(255, 255, 255, 50);

        private static Random random = new Random(DateTime.Now.Millisecond);

        public TexturedFieldCellDrawable(FieldCell cell, int cellSize)
        {
            _cell = cell;

            _backgroundSprite = new Sprite();

            var textureIndex = random.Next(0, 3); ;
            _backgroundSprite.Texture = Game.Instance.TextureCache.GetOrCache($"squareplate{textureIndex}");
            var scaleFactor = (float)cellSize / _backgroundSprite.Texture.Size.X;
            _backgroundSprite.Scale = new Vector2f(scaleFactor, scaleFactor);

            _hoveredEffectRectangle = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = HoveredColor
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
            if(_isHoveredVisible) target.Draw(_hoveredEffectRectangle);
        }

        private void Update()
        {
            SetHoveredVisible(_cell.Hovered);
            SetFlagsVisibility(_cell.Type);
            SetClicked(_cell.Clicked);
        }

        private void SetHoveredVisible(bool visible)
        {
            _isHoveredVisible = visible;
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
                _backgroundSprite.Color = ClickedColor;
            }
            else
            {
                _backgroundSprite.Color = DefaultBackgroundColor;
            }
        }
    }
}
using System;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Cells;
using KragmortaApp.Enums;
using KragmortaApp.Presenters;
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
        // protected RectangleShape _red;
        protected Sprite _red;

        /// <summary>
        /// Green sub-rect
        /// </summary>
        protected Sprite _green;
        // protected RectangleShape _green;

        /// <summary>
        /// Blue sub-rect
        /// </summary>
        // protected RectangleShape _blue;
        protected Sprite _blue;

        /// <summary>
        /// Orange sub-rect
        /// </summary>
        // protected RectangleShape _orange;
        protected Sprite _orange;
        
        /// <summary>
        /// Common sub-rect
        /// </summary>
        protected Sprite _common;


        // Visibility flags

        protected bool _isRedVisible;
        protected bool _isGreenVisible;
        protected bool _isBlueVisible;
        protected bool _isOrangeVisible;
        protected bool _isCommonVisible;
        protected bool _isHoveredVisible;

        protected static readonly Color DefaultBackgroundColor = new Color(255, 255, 255, 255);
        protected static readonly Color ClickedColor = new Color(127, 127, 127);
        protected static readonly Color HoveredColor = new Color(255, 255, 255, 50);

        protected static Random random = new Random(DateTime.Now.Millisecond);

        private readonly string appendix;

        public TexturedFieldCellDrawable(FieldCell cell, int cellSize)
        {
            _cell    = cell;
            appendix = _cell.IsPortal ? "_portal" : "";

            _backgroundSprite       = new Sprite();
            _hoveredEffectRectangle = new Sprite();

            switch (cell.Form)
            {
                case CellForm.Big:
                {
                    InitBig(cell.Corner, cellSize);
                    break;
                }
                case CellForm.Small:
                {
                    InitSmall(cell.Corner, cellSize);
                    break;
                }
                case CellForm.Square:
                {
                    InitSquare(cellSize);
                    break;
                }
                default:
                {
                    throw new KragException($"Unknown type of the cell X: {cell.X}, Y: {cell.Y}");
                }
            }
        }

        private void InitBig(Corner corner, int cellSize)
        {
            var textureIndex = random.Next(0, 3);

            switch (corner)
            {
                case Corner.TopLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlate/bigtrapezeplate{textureIndex}{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlate/bigtrapezeplate{textureIndex}{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlate/bigtrapezeplateline");
                    break;
                }
                case Corner.TopRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateRef/bigtrapezeplate{textureIndex}ref{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateRef/bigtrapezeplate{textureIndex}ref{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateRef/bigtrapezeplatelineref");
                    break;
                }
                case Corner.BottomLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateRefReversed/bigtrapezeplate{textureIndex}refreversed{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateRefReversed/bigtrapezeplate{textureIndex}refreversed{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateRefReversed/bigtrapezeplatelinerefreversed");
                    break;
                }
                case Corner.BottomRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateReversed/bigtrapezeplate{textureIndex}reversed{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateReversed/bigtrapezeplate{textureIndex}reversed{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/BigTrapezePlateReversed/bigtrapezeplatelinereversed");
                    break;
                }
                default:
                {
                    _backgroundSprite.Color = Color.Red;
                    break;
                }
            }

            const int offset = 15;
            cellSize += offset;

            var downscaleFactor = (float)cellSize / _backgroundSprite.Texture.Size.X;
            _hoveredEffectRectangle.Scale = _backgroundSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);


            var positionX =
                CellPresenterAbstract.FieldOriginX +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.X;
            var positionY =
                CellPresenterAbstract.FieldOriginY +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.Y;

            if (corner is Corner.BottomRight or Corner.TopRight)
            {
                positionX -= offset;
            }

            if (corner is Corner.BottomLeft or Corner.BottomRight)
            {
                positionY -= 18;
            }

            InitRedIcon();
            InitGreenIcon();
            InitBlueIcon();
            InitOrangeIcon();
            InitCommonIcon();

            SetPosition(positionX, positionY);
        }

        private void InitCommonIcon()
        {
            _common          = new Sprite(Engine.Instance.TextureCache.GetOrCache("Common"));
            _common.Scale    = new Vector2f(0.6f, 0.6f);
            // _orange.Rotation = random.Next(2) % 2 == 0 ? -15f : 15f;
        }

        private void InitOrangeIcon()
        {
            _orange          = new Sprite(Engine.Instance.TextureCache.GetOrCache("Orange"));
            _orange.Scale    = new Vector2f(0.6f, 0.6f);
            _orange.Rotation = random.Next(2) % 2 == 0 ? -15f : 15f;
        }

        private void InitBlueIcon()
        {
            _blue          = new Sprite(Engine.Instance.TextureCache.GetOrCache("Blue"));
            _blue.Scale    = new Vector2f(0.65f, 0.65f);
            _blue.Rotation = random.Next(2) % 2 == 0 ? -15f : 15f;
        }

        private void InitGreenIcon()
        {
            _green          = new Sprite(Engine.Instance.TextureCache.GetOrCache("Green"));
            _green.Scale    = new Vector2f(0.6f, 0.6f);
            _green.Rotation = random.Next(2) % 2 == 0 ? -15f : 15f;
        }

        private void InitRedIcon()
        {
            _red          = new Sprite(Engine.Instance.TextureCache.GetOrCache("Red"));
            _red.Scale    = new Vector2f(0.5f, 0.5f);
            _red.Rotation = random.Next(2) % 2 == 0 ? -15f : 15f;
        }

        private void InitSmall(Corner corner, int cellSize)
        {
            var textureIndex = random.Next(0, 3);

            switch (corner)
            {
                case Corner.TopLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlate/smalltrapezeplate{textureIndex}{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlate/smalltrapezeplate{textureIndex}{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlate/smalltrapezeplateline");
                    break;
                }
                case Corner.TopRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateRef/smalltrapezeplate{textureIndex}ref{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateRef/smalltrapezeplate{textureIndex}ref{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateRef/smalltrapezeplatelineref");
                    break;
                }
                case Corner.BottomLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateRefReversed/smalltrapezeplate{textureIndex}refreversed{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateRefReversed/smalltrapezeplate{textureIndex}refreversed{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateRefReversed/smalltrapezeplatelinerefreversed");
                    break;
                }
                case Corner.BottomRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateReversed/smalltrapezeplate{textureIndex}reversed{appendix}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateReversed/smalltrapezeplate{textureIndex}reversed{appendix}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/SmallTrapezePlateReversed/smalltrapezeplatelinereversed");
                    break;
                }
                default:
                {
                    _backgroundSprite.Color = Color.Red;
                    break;
                }
            }

            var downscaleFactor = (float)cellSize / _backgroundSprite.Texture.Size.X;
            _hoveredEffectRectangle.Scale = _backgroundSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);

            InitRedIcon();
            InitGreenIcon();
            InitBlueIcon();
            InitOrangeIcon();
            InitCommonIcon();

            var positionX =
                CellPresenterAbstract.FieldOriginX +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.X;
            var positionY =
                CellPresenterAbstract.FieldOriginY +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.Y;

            SetPosition(positionX, positionY);
        }

        private void InitSquare(int cellSize)
        {
            var textureIndex = random.Next(0, 3);


            _backgroundSprite.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplate{textureIndex}{appendix}");
            _backgroundImage =
                Engine.Instance.ImageCache.GetOrCache($"squareplate/squareplate{textureIndex}{appendix}");
            _hoveredEffectRectangle.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplateline");

            var downscaleFactor = (float)cellSize / _backgroundSprite.Texture.Size.X;
            _hoveredEffectRectangle.Scale = _backgroundSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);


            InitRedIcon();
            InitGreenIcon();
            InitBlueIcon();
            InitOrangeIcon();
            InitCommonIcon();


            var positionX =
                CellPresenterAbstract.FieldOriginX +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.X;
            var positionY =
                CellPresenterAbstract.FieldOriginY +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.Y;

            SetPosition(positionX, positionY);
        }

        public void SetPosition(int x, int y)
        {
            _backgroundSprite.Position       = new Vector2f(x, y);
            _hoveredEffectRectangle.Position = new Vector2f(x, y);
            _red.Position                    = new Vector2f(x + 10, y + 10);
            _green.Position                  = new Vector2f(x + 15, y + 15);
            _blue.Position                   = new Vector2f(x + 50, y + 10);
            _orange.Position                 = new Vector2f(x + 70, y + 10);
            _common.Position                 = new Vector2f(x + 10, y + 10);
        }

        public void ShiftPosition(int x, int y)
        {
            var shiftVector = new Vector2f(x, y);
            _backgroundSprite.Position       += shiftVector;
            _hoveredEffectRectangle.Position += shiftVector;
            _red.Position                    += shiftVector;
            _green.Position                  += shiftVector;
            _blue.Position                   += shiftVector;
            _orange.Position                 += shiftVector;
            _common.Position                 += shiftVector;
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
            if (_isCommonVisible) target.Draw(_common);
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
            _isCommonVisible = (type & CellType.Common) == CellType.Common;
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
                       _backgroundSprite.GetGlobalBounds().Width) && !(y < _backgroundSprite.GetGlobalBounds().Top) &&
                   !(y > _backgroundSprite.GetGlobalBounds().Top + _backgroundSprite.GetGlobalBounds().Height);
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

            return pixel.A == 0;
        }
    }
}
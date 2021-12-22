using System;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using KragmortaApp.Presenters;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class AbstractCellDrawable : Drawable
    {
        /// <summary>
        /// Color to fill the object.
        /// </summary>
        protected Color HighlightedColor => _highlightedColor;

        private Color _highlightedColor;

        /// <summary>
        /// Model which is drawn.
        /// </summary>
        protected AbstractCell _cell;

        /// <summary>
        /// Sprite for any effects, applied to effects.
        /// </summary>
        protected Sprite _effectSprite;

        /// <summary>
        /// The flag detecting whether the cell is visible on screen.
        /// </summary>
        protected bool _visible;

        private Random _random;

        /// <summary>
        /// Init new abstract cell which will be drawn on the screen.
        /// </summary>
        /// <param name="cell">Model of the cell</param>
        /// <param name="cellSize">Dimensions of the cell on screen</param>
        /// <param name="highlightedColor">Color to fill the cell</param>
        public AbstractCellDrawable(AbstractCell cell, int cellSize, Color highlightedColor)
        {
            _highlightedColor = highlightedColor;
            _cell             = cell;
            _random           = new Random(DateTime.Now.Millisecond);

            _effectSprite = new Sprite();

            InitCellByForm(cell, cellSize);
        }

        protected void InitCellByForm(AbstractCell cell, int cellSize)
        {
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
                case CellForm.Unset: break;
                default:
                {
                    throw new KragException($"Unknown Form of the cell X: {cell.X}, Y: {cell.Y}");
                }
            }
        }

        private protected void InitBig(Corner corner, int cellSize)
        {
            const int offset = 15;
            cellSize += offset;

            var downscaleFactor = (float)cellSize / _effectSprite.Texture.Size.X;
            _effectSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);

            _effectSprite.TextureRect =
                new IntRect(0, 0, (int)_effectSprite.Texture.Size.X, (int)_effectSprite.Texture.Size.Y);


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

            SetPosition(positionX, positionY);
        }

        protected virtual void LoadBigTexture(Corner corner)
        {
        }

        protected virtual void LoadSmallTexture(Corner corner)
        {
        }

        protected virtual void LoadSquareTexture()
        {
        }

        private protected void InitSmall(Corner corner, int cellSize)
        {
            var downscaleFactor = (float)cellSize / _effectSprite.Texture.Size.X;
            _effectSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);

            var positionX =
                CellPresenterAbstract.FieldOriginX +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.X;
            var positionY =
                CellPresenterAbstract.FieldOriginY +
                (CellPresenterAbstract.CellSize + CellPresenterAbstract.CellMargin) * _cell.Y;

            SetPosition(positionX, positionY);
        }

        private protected void InitSquare(int cellSize)
        {
            var downscaleFactor = (float)cellSize / _effectSprite.Texture.Size.X;
            _effectSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);

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
            _effectSprite.Position = new Vector2f(x, y);
        }

        public void ShiftPosition(int x, int y)
        {
            var shiftVector = new Vector2f(x, y);
            _effectSprite.Position += shiftVector;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_cell.Dirty)
            {
                Update();
                _cell.ClearDirty();
            }

            if (!_visible) return;
            target.Draw(_effectSprite);
        }

        private void Update()
        {
            _visible = _cell.Visible;
        }
    }
}
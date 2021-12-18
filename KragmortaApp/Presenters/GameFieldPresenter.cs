using System;
using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Drawables.FieldCellDrawables;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class GameFieldPresenter : CellPresenterAbstract
    {
        private readonly GameField _field;

        private List<TexturedFieldCellDrawable> _drawables;

        private readonly string ErrorMsg =
            "Ебать мой хуй поле у тебя кусок хуеты, с размерами которой я не ебу шо делать... поэтому лови исключение, уебан(ка)!";

        public GameFieldPresenter(GameField field)
        {
            _field     = field;
            _drawables = InitTexturedFieldCellDrawables(field.Cells.Count);

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            for (var i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].ShiftPosition(x, y);
            }
        }

        private List<TexturedFieldCellDrawable> InitTexturedFieldCellDrawables(int count)
        {
            var result = new List<TexturedFieldCellDrawable>(70);

            if (GameState.Instance.Field.SizeX == 10)
            {
                InitField10X7(result);
            }
            else if (GameState.Instance.Field.SizeX == 7)
            {
                InitField7X10(result);
            }
            else
            {
                throw new KragException(ErrorMsg);
            }

            return result;
        }

        private void InitField10X7(List<TexturedFieldCellDrawable> result)
        {
            #region 1st row

            result.Add(InitBigPolynomial(_field.Cells[0], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[1], Corner.TopRight));
            result.Add(InitSmallPolynomial(_field.Cells[2], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[3], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[4]));
            result.Add(InitSquare(_field.Cells[5]));

            result.Add(InitBigPolynomial(_field.Cells[6], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[7], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[8]));
            result.Add(InitSquare(_field.Cells[9]));

            #endregion

            #region 2nd row

            result.Add(InitSmallPolynomial(_field.Cells[10], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[11], Corner.BottomRight));
            result.Add(InitBigPolynomial(_field.Cells[12], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[13], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[14]));
            result.Add(InitSquare(_field.Cells[15]));

            result.Add(InitSmallPolynomial(_field.Cells[16], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[17], Corner.BottomRight));

            result.Add(InitBigPolynomial(_field.Cells[18], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[19], Corner.TopRight));

            #endregion

            #region 3rd row

            result.Add(InitBigPolynomial(_field.Cells[20], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[21], Corner.TopRight));
            result.Add(InitSmallPolynomial(_field.Cells[22], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[23], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[24]));
            result.Add(InitSquare(_field.Cells[25]));

            result.Add(InitBigPolynomial(_field.Cells[26], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[27], Corner.TopRight));

            result.Add(InitSmallPolynomial(_field.Cells[28], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[29], Corner.BottomRight));

            #endregion

            #region 4th row

            result.Add(InitSmallPolynomial(_field.Cells[30], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[31], Corner.BottomRight));
            result.Add(InitBigPolynomial(_field.Cells[32], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[33], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[34]));
            result.Add(InitSquare(_field.Cells[35]));

            result.Add(InitSmallPolynomial(_field.Cells[36], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[37], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[38]));
            result.Add(InitSquare(_field.Cells[39]));
            // result.Add(InitSmallPolynomial(_field.Cells[38], Corner.TopLeft));
            // result.Add(InitBigPolynomial(_field.Cells[39], Corner.TopRight));

            #endregion

            #region 5th row

            result.Add(InitBigPolynomial(_field.Cells[40], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[41], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[42]));
            result.Add(InitSquare(_field.Cells[43]));

            result.Add(InitSquare(_field.Cells[44]));
            result.Add(InitSquare(_field.Cells[45]));

            result.Add(InitSquare(_field.Cells[46]));
            result.Add(InitSmallPolynomial(_field.Cells[47], Corner.TopLeft));

            result.Add(InitBigPolynomial(_field.Cells[48], Corner.TopRight));
            result.Add(InitSquare(_field.Cells[49]));

            #endregion

            #region 6th row

            result.Add(InitSmallPolynomial(_field.Cells[50], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[51], Corner.BottomRight));

            result.Add(InitSmallPolynomial(_field.Cells[52], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[53], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[54]));
            result.Add(InitBigPolynomial(_field.Cells[55], Corner.TopLeft));

            result.Add(InitSmallPolynomial(_field.Cells[56], Corner.TopRight));
            result.Add(InitBigPolynomial(_field.Cells[57], Corner.BottomLeft));

            result.Add(InitSmallPolynomial(_field.Cells[58], Corner.BottomRight));
            result.Add(InitSquare(_field.Cells[59]));

            #endregion

            #region 7th row

            result.Add(InitSquare(_field.Cells[60]));
            result.Add(InitSquare(_field.Cells[61]));

            result.Add(InitBigPolynomial(_field.Cells[62], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[63], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[64]));
            result.Add(InitSmallPolynomial(_field.Cells[65], Corner.BottomLeft));

            result.Add(InitBigPolynomial(_field.Cells[66], Corner.BottomRight));
            result.Add(InitSquare(_field.Cells[67]));

            result.Add(InitSquare(_field.Cells[68]));
            result.Add(InitSquare(_field.Cells[69]));

            #endregion
        }

        private void InitField7X10(List<TexturedFieldCellDrawable> result)
        {
            int i = 0;

            #region 1st row

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            #endregion

            #region 2nd row

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            #endregion

            #region 3rd row

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopRight));

            #endregion

            #region 4th row

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomRight));

            #endregion

            #region 5th row

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSquare(_field.Cells[i++]));

            #endregion

            #region 6th row

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSquare(_field.Cells[i++]));

            #endregion

            #region 7th row

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            #endregion

            #region 8th row

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            #endregion

            #region 9th row

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.TopLeft));
            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.TopRight));

            result.Add(InitSquare(_field.Cells[i++]));

            #endregion

            #region 10th row

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitSquare(_field.Cells[i++]));
            result.Add(InitSquare(_field.Cells[i++]));

            result.Add(InitBigPolynomial(_field.Cells[i++], Corner.BottomLeft));
            result.Add(InitSmallPolynomial(_field.Cells[i++], Corner.BottomRight));

            result.Add(InitSquare(_field.Cells[i]));

            #endregion
        }

        private BigPolygonTexturedFieldCellDrawable InitBigPolynomial(FieldCell cell, Corner corner)
        {
            const int k = 15;

            var drawable = new BigPolygonTexturedFieldCellDrawable(cell, CellSize + k, corner);

            var positionX = FieldOriginX + (CellSize + CellMargin) * cell.X;
            var positionY = FieldOriginY + (CellSize + CellMargin) * cell.Y;

            if (corner is Corner.BottomRight or Corner.TopRight)
            {
                positionX -= k;
            }

            if (corner is Corner.BottomLeft or Corner.BottomRight)
            {
                positionY -= 18;
            }

            drawable.SetPosition(positionX, positionY);
            return drawable;
        }

        private SmallPolygonTexturedFieldCellDrawable InitSmallPolynomial(FieldCell cell, Corner corner)
        {
            var drawable = new SmallPolygonTexturedFieldCellDrawable(cell, CellSize, corner);

            var positionX = FieldOriginX + (CellSize + CellMargin) * cell.X;
            var positionY = FieldOriginY + (CellSize + CellMargin) * cell.Y;

            drawable.SetPosition(positionX, positionY);
            return drawable;
        }

        private SquareTexturedFieldCellDrawable InitSquare(FieldCell cell)
        {
            var drawable = new SquareTexturedFieldCellDrawable(cell, CellSize);

            var positionX = FieldOriginX + (CellSize + CellMargin) * cell.X;
            var positionY = FieldOriginY + (CellSize + CellMargin) * cell.Y;

            drawable.SetPosition(positionX, positionY);
            return drawable;
        }

        public override void Render(RenderTarget target)
        {
            if (_field.Dirty)
            {
                _field.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }

        /// <summary>
        /// Ensures that x and y coordinates are within the presented game field
        /// </summary>
        public override bool IsMouseWithinBounds(int x, int y)
        {
            return !(x < FieldOriginX ||
                     x >= FieldOriginX + (CellSize + CellMargin) * _field.SizeX ||
                     y < FieldOriginY ||
                     y >= FieldOriginY + (CellSize + CellMargin) * _field.SizeY
                );
        }

        public bool TryConvertMouseCoordsToCellCoords(int x, int y, out int cellX, out int cellY)
        {
            foreach (var drawable in _drawables)
            {
                if (drawable.IsMouseWithinBounds(x, y))
                {
                    if (drawable.IsTransparentPixel(x, y)) continue;

                    cellX = drawable.Cell.X;
                    cellY = drawable.Cell.Y;

                    return true;
                }
            }

            cellX = cellY = -1;
            return false;
        }
    }
}
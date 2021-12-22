using System;
using System.Collections.Generic;
using KragmortaApp.Enums;

namespace KragmortaApp.Entities
{
    public class GameField : VisualEntity
    {
        public readonly int SizeX;
        public readonly int SizeY;
        public IReadOnlyList<FieldCell> Cells => _cells;

        private readonly List<FieldCell> _cells;

        private readonly string ErrorMsg =
            "Ебать мой хуй поле у тебя кусок хуеты, с размерами которой я не ебу шо делать... поэтому лови исключение, уебан(ка)!";

        private static Random _random;

        public GameField(int sizeX, int sizeY)
        {
            SizeX  = sizeX;
            SizeY  = sizeY;
            _cells = new List<FieldCell>(sizeX * sizeY);

            _random = new Random(DateTime.Now.Millisecond);


            if (sizeX == 10 && sizeY == 7)
            {
                InitField10X7();
            }
            else if (sizeX == 7 && sizeY == 10)
            {
                InitField7X10();
            }
            else
            {
                throw new KragException(ErrorMsg);
            }
        }

        private void InitField10X7()
        {
            int x = 0, y = 0;

            #region 1st row

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 2nd row

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            x = 0;
            ++y;

            #endregion

            #region 3rd row

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            x = 0;
            ++y;

            #endregion

            #region 4th row

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 5th row

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));
            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 6th row

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));
            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 7th row

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x, y));

            #endregion
        }

        private void InitField7X10()
        {
            int x = 0, y = 0;

            #region 1st row

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x, y, Corner.TopRight));

            x = 0;
            ++y;

            #endregion

            #region 2nd row

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            x = 0;
            ++y;

            #endregion

            #region 3rd row

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            x = 0;
            ++y;

            #endregion

            #region 4th row

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            x = 0;
            ++y;

            #endregion

            #region 5th row

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 6th row

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 7th row

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            x = 0;
            ++y;

            #endregion

            #region 8th row

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            x = 0;
            ++y;

            #endregion

            #region 9th row

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSmallPolynomial(x++, y, Corner.TopLeft));
            _cells.Add(InitBigPolynomial(x++, y, Corner.TopRight));

            _cells.Add(InitSquare(x++, y));

            x = 0;
            ++y;

            #endregion

            #region 10th row

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitSquare(x++, y));
            _cells.Add(InitSquare(x++, y));

            _cells.Add(InitBigPolynomial(x++, y, Corner.BottomLeft));
            _cells.Add(InitSmallPolynomial(x++, y, Corner.BottomRight));

            _cells.Add(InitSquare(x, y));

            #endregion
        }

        private FieldCell InitBigPolynomial(int x, int y, Corner corner)
        {
            return new FieldCell()
            {
                X      = x,
                Y      = y,
                Type   = (CellType)(1 << _random.Next(4)),
                Corner = corner,
                Form   = CellForm.Big
            };
        }

        private FieldCell InitSmallPolynomial(int x, int y, Corner corner)
        {
            return new FieldCell()
            {
                X      = x,
                Y      = y,
                Type   = (CellType)(1 << _random.Next(4)),
                Corner = corner,
                Form   = CellForm.Small
            };
        }

        private FieldCell InitSquare(int x, int y)
        {
            return new FieldCell()
            {
                X      = x,
                Y      = y,
                Type   = (CellType)(1 << _random.Next(4)),
                Corner = Corner.None,
                Form   = CellForm.Square
            };
        }

        /// <summary>
        /// Retrieves field cell by it's field coordinates.
        /// <remarks>Doesn't perform any checks for indices</remarks>
        /// </summary>
        public FieldCell GetCell(int cX, int cY)
        {
            return _cells[GetCellIndex(cX, cY)];
        }

        /// <summary>
        /// Retrieves field cell sequential index by it's field coordinates.
        /// </summary>
        public int GetCellIndex(int cX, int cY)
        {
            return cX + cY * SizeX;
        }
    }
}
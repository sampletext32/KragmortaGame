using System;
using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Enums;
using KragmortaApp.FileDatas;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Entities
{
    public class GameField : VisualEntity
    {
        public readonly int SizeX;
        public readonly int SizeY;
        public IReadOnlyList<FieldCell> Cells => _cells;

        private readonly List<FieldCell> _cells;

        public FieldType FieldType;

        private readonly string ErrorMsg =
            "Ебать мой хуй поле у тебя кусок хуеты, с размерами которой я не ебу шо делать... поэтому лови исключение, уебан(ка)!";

        private static Random _random;

        private int _playersCount;

        public GameField(GameFieldFileData fileData)
        {
            SizeX         = fileData.SizeX;
            SizeY         = fileData.SizeY;
            FieldType     = fileData.FieldType;
            _cells        = fileData.Cells.Select(c => new FieldCell(c)).ToList();
            _playersCount = fileData.PlayersCount;
        }

        public GameFieldFileData ToFileData()
        {
            return new GameFieldFileData()
            {
                FieldType    = FieldType,
                SizeX        = SizeX,
                SizeY        = SizeY,
                PlayersCount = _playersCount,
                Cells        = _cells.Select(c => c.ToFileData()).ToList()
            };
        }

        public GameField(int sizeX, int sizeY, int playersCount)
        {
            SizeX         = sizeX;
            SizeY         = sizeY;
            _playersCount = playersCount;
            _cells        = new List<FieldCell>(sizeX * sizeY);

            _random = new Random(DateTime.Now.Millisecond);


            if (sizeX == 10 && sizeY == 7)
            {
                InitField10X7();
                AdjustField();
                InitWalls();
            }
            else if (sizeX == 7 && sizeY == 10)
            {
                InitField7X10();
                AdjustField();
                InitWalls();
            }
            else
            {
                throw new KragException(ErrorMsg);
            }
        }

        private void AdjustField()
        {
            if (_playersCount <= 4)
            {
                FieldType = FieldType.Mini;
                SetMiniField();
            }
            else if (_playersCount <= 6)
            {
                FieldType = FieldType.Medium;
                SetMediumField();
            }
            else if (_playersCount <= 8)
            {
                FieldType = FieldType.Large;
            }
            else
            {
                throw new KragException($"Invalid number of player: {_playersCount}");
            }
        }

        private void SetMediumField()
        {
            for (int i = 0; i < SizeX; i++)
            {
                _cells[i].Visible = false;
            }

            _cells[2 * SizeX - 1].Visible = false;
            _cells[2 * SizeX - 2].Visible = false;
        }

        private void SetMiniField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    _cells[SizeX * i + j].Visible = false;
                }
            }
        }

        private void InitWalls()
        {
            // TODO: Show bookshelves on the field.
            switch (FieldType)
            {
                case FieldType.Mini:
                {
                    for (int i = 0; i < SizeX; i++)
                    {
                        _cells[SizeX * 3 + i].Type    = CellType.Wall;
                        _cells[SizeX * 3 + i].Visible = false;
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        _cells[SizeX * (5 + i) - 1].Type    = CellType.Wall;
                        _cells[SizeX * (5 + i) - 1].Visible = false;
                    }

                    break;
                }

                case FieldType.Medium:
                {
                    int i;
                    for (i = 0; i < 5; i++)
                    {
                        _cells[SizeX + i].Type    = CellType.Wall;
                        _cells[SizeX + i].Visible = false;
                    }

                    for (--i; i < SizeX; i++)
                    {
                        _cells[SizeX * 2 + i].Type    = CellType.Wall;
                        _cells[SizeX * 2 + i].Visible = false;
                    }

                    _cells[SizeX * 4 + 2].Visible = false;
                    _cells[SizeX * 4 + 2].Type    = CellType.Wall;
                    _cells[SizeX * 5 + 2].Visible = false;
                    _cells[SizeX * 5 + 2].Type    = CellType.Wall;

                    _cells[SizeX * 4 + 4].Visible = false;
                    _cells[SizeX * 4 + 4].Type    = CellType.Wall;
                    _cells[SizeX * 5 + 4].Type    = CellType.Wall;
                    _cells[SizeX * 5 + 4].Visible = false;


                    break;
                }

                case FieldType.Large:
                {
                    _cells[SizeX + 2].Type        = CellType.Wall;
                    _cells[SizeX + 2].Visible     = false;
                    _cells[SizeX * 2 + 2].Type    = CellType.Wall;
                    _cells[SizeX * 2 + 2].Visible = false;
                    
                    _cells[SizeX + 4].Type        = CellType.Wall;
                    _cells[SizeX + 4].Visible     = false;
                    _cells[SizeX * 2 + 4].Type    = CellType.Wall;
                    _cells[SizeX * 2 + 4].Visible = false;
                    
                    _cells[SizeX * 4 + 2].Type    = CellType.Wall;
                    _cells[SizeX * 4 + 2].Visible = false;
                    _cells[SizeX * 5 + 2].Type    = CellType.Wall;
                    _cells[SizeX * 5 + 2].Visible = false;
                    
                    _cells[SizeX * 4 + 4].Type    = CellType.Wall;
                    _cells[SizeX * 4 + 4].Visible = false;
                    _cells[SizeX * 5 + 4].Type    = CellType.Wall;
                    _cells[SizeX * 5 + 4].Visible = false;
                    
                    _cells[SizeX * 7 + 3].Type    = CellType.Wall;
                    _cells[SizeX * 7 + 3].Visible = false;
                    _cells[SizeX * 8 + 3].Type    = CellType.Wall;
                    _cells[SizeX * 8 + 3].Visible = false;
                    
                    
                    break;
                }
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


            GetCell(0, 0).IsPortal = true;
            GetCell(6, 0).IsPortal = true;
            GetCell(3, 2).IsPortal = true;
            GetCell(1, 4).IsPortal = true;
            GetCell(5, 4).IsPortal = true;
            GetCell(4, 6).IsPortal = true;
            GetCell(6, 9).IsPortal = true;
        }

        private FieldCell InitBigPolynomial(int x, int y, Corner corner)
        {
            return new FieldCell()
            {
                X       = x,
                Y       = y,
                Type    = (CellType)(1 << _random.Next(4)),
                Corner  = corner,
                Form    = CellForm.Big,
                Visible = true
            };
        }

        private FieldCell InitSmallPolynomial(int x, int y, Corner corner)
        {
            return new FieldCell()
            {
                X       = x,
                Y       = y,
                Type    = (CellType)(1 << _random.Next(4)),
                Corner  = corner,
                Form    = CellForm.Small,
                Visible = true
            };
        }

        private FieldCell InitSquare(int x, int y)
        {
            return new FieldCell()
            {
                X       = x,
                Y       = y,
                Type    = (CellType)(1 << _random.Next(4)),
                Corner  = Corner.None,
                Form    = CellForm.Square,
                Visible = true
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
        /// Retrieves field cell by it's field coordinates.
        /// <remarks>Doesn't perform any checks for indices</remarks>
        /// </summary>
        public FieldCell GetCell(Vector2i coords)
        {
            return _cells[GetCellIndex(coords.X, coords.Y)];
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
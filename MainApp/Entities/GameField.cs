using System;
using System.Collections.Generic;
using MainApp.Entities.Enums;
using MainApp.Entities.Models;
using SFML.Graphics;

namespace MainApp.Entities
{
    public class GameField
    {
        public readonly int SizeX;
        public readonly int SizeY;
        public IReadOnlyList<FieldCell> Cells => _cells;

        private readonly List<FieldCell> _cells;

        public GameField(int sizeX, int sizeY)
        {
            SizeX  = sizeX;
            SizeY  = sizeY;
            _cells = new List<FieldCell>(sizeX * sizeY);
            var random = new Random(DateTime.Now.Millisecond);

            // Here we write cells with row-first order, so that addressing is correct
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    _cells.Add(
                        new FieldCell()
                        {
                            X    = j,
                            Y    = i,
                            Type = (CellType)(1 << random.Next(4))
                        }
                    );
                }
            }
        }

        /// <summary>
        /// Retrieves field cell by it's field coordinates.
        /// <remarks>Doesn't make any checks of coordinates</remarks>
        /// </summary>
        public FieldCell GetCell(int cX, int cY)
        {
            return _cells[cX + cY * SizeX];
        }
    }
}
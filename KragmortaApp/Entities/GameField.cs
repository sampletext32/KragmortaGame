﻿using System;
using System.Collections.Generic;
using KragmortaApp.Enums;
using SFML.Graphics;

namespace KragmortaApp.Entities
{
    public class GameField : VisualEntity
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
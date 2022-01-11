using System;
using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class GameFieldController : ControllerBase
    {
        private FieldCell _lastHoveredCell = null;

        private FieldCell _lastPressedCell = null;

        private readonly GameField _field;

        private readonly Random _random;

        public GameFieldController(GameField field, bool initStates)
        {
            _field  = field;
            _random = new Random(DateTime.Now.Millisecond);
            _field = field;

            if (initStates)
            {
            }
            else
            {
                _lastHoveredCell = field.Cells.FirstOrDefault(c => c.Hovered);
                _lastPressedCell = field.Cells.FirstOrDefault(c => c.Clicked);
            }
        }

        public void ClearLastHoveredCell()
        {
            if (_lastHoveredCell is not null)
            {
                _lastHoveredCell.Hovered = false;
                _lastHoveredCell.MarkDirty();
                _lastHoveredCell = null;
            }
        }

        public FieldCell GetCell(int cellX, int cellY)
        {
            return _field.GetCell(cellX, cellY);
        }

        public void HoverCell(FieldCell cell)
        {
            if (cell == _lastHoveredCell)
            {
                return;
            }

            if (_lastHoveredCell is not null)
            {
                _lastHoveredCell.Hovered = false;
                _lastHoveredCell.MarkDirty();
            }

            cell.Hovered = true;
            cell.MarkDirty();
            _lastHoveredCell = cell;
        }

        public void PressCell(FieldCell fieldCell)
        {
            fieldCell.Clicked = true;
            fieldCell.Dirty   = true;

            _lastPressedCell = fieldCell;
        }

        public void ReleaseLastPressedCell()
        {
            if (_lastPressedCell is not null)
            {
                _lastPressedCell.Clicked = false;
                _lastPressedCell.MarkDirty();
                _lastPressedCell = null;
            }
        }

        public void CollectNeighboringCells(int centerCellX, int centerCellY, List<AbstractCell> rawPaths)
        {
            if (centerCellX != _field.SizeX - 1)
            {
                rawPaths.Add(GetCell(centerCellX + 1, centerCellY));
            }

            if (centerCellX != 0)
            {
                rawPaths.Add(GetCell(centerCellX - 1, centerCellY));
            }

            if (centerCellY != 0)
            {
                rawPaths.Add(GetCell(centerCellX, centerCellY - 1));
            }

            if (centerCellY != _field.SizeY - 1)
            {
                rawPaths.Add(GetCell(centerCellX, centerCellY + 1));
            }
        }

        public FieldCell GetSpawnCell()
        {
            return _field.SpawnCells[_random.Next(0, _field.SpawnCells.Count)];
        }
    }
}
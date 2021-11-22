using System.Collections.Generic;
using System.Linq;
using MainApp.Entities;

namespace MainApp.Controllers
{
    public class GameFieldController : ControllerBase
    {
        private FieldCell _lastHoveredCell = null;

        private FieldCell _lastPressedCell = null;
        
        private readonly GameField _field;

        public GameFieldController(GameField field)
        {
            _field = field;
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

        public void CollectNeighboringCells(int heroX, int heroY, List<AbstractCell> rawPaths)
        {
            if (heroX != _field.SizeX - 1)
            {
                rawPaths.Add(GetCell(heroX + 1, heroY));
            }

            if (heroX != 0)
            {
                rawPaths.Add(GetCell(heroX - 1, heroY));
            }

            if (heroY != 0)
            {
                rawPaths.Add(GetCell(heroX, heroY - 1));
            }

            if (heroY != _field.SizeY - 1)
            {
                rawPaths.Add(GetCell(heroX, heroY + 1));
            }
        }
    }
}
using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Enums;

namespace KragmortaApp.Controllers
{
    public class PathController : ControllerBase
    {
        private Path _path;

        public PathController(Path path)
        {
            _path = path;
        }

        public bool TryGetCell(int selectedCellX, int selectedCellY, out PathCell pathCell)
        {
            foreach (var cell in _path.Cells)
            {
                if (cell.X == selectedCellX && cell.Y == selectedCellY && cell.Visible)
                {
                    pathCell = cell;
                    return true;
                }
            }

            pathCell = null;
            return false;
        }

        public void SetVisiblePath(List<AbstractCell> cells, MovementCard card)
        {
            if (cells.Count > 4)
            {
                throw new KragException("Found path cells are supposed to not exceed a count of 4");
            }

            // highlight paths
            for (int i = 0; i < cells.Count; i++)
            {
                if (!card.HasUsedFirstType && (cells[i].Type & card.FirstType) != CellType.Empty ||
                    !card.HasUsedSecondType && (cells[i].Type & card.SecondType) != CellType.Empty)
                {
                    _path.Cells[i].X       = cells[i].X;
                    _path.Cells[i].Y       = cells[i].Y;
                    _path.Cells[i].Type    = cells[i].Type;
                    _path.Cells[i].Visible = true;
                }
                else
                {
                    _path.Cells[i].Visible = false;
                }

                _path.Cells[i].MarkDirty();
            }

            // hide path cells, which have no field cell attached
            for (int i = cells.Count; i < 4; i++)
            {
                _path.Cells[i].Visible = false;

                _path.Cells[i].MarkDirty();
            }

            _path.MarkDirty();
        }
    }
}
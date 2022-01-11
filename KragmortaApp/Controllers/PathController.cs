using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Enums;

namespace KragmortaApp.Controllers
{
    public class PathController : ControllerBase
    {
        public List<AbstractCell> RawPath = new List<AbstractCell>(4);
        
        private Path _path;

        public PathController(Path path, bool initStates)
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

        public bool TrySetVisiblePath(MovementCard card)
        {
            if (RawPath.Count > 4)
            {
                throw new KragException("Found path cells are supposed to not exceed a count of 4");
            }

            bool hasSet = false;

            // highlight paths
            for (int i = 0; i < RawPath.Count; i++)
            {
                if (RawPath[i].Type == CellType.Wall)
                {
                    _path.Cells[i].Visible = false;
                    continue;
                }
                
                if (IsValid(card, i))
                {
                    _path.Cells[i].X       = RawPath[i].X;
                    _path.Cells[i].Y       = RawPath[i].Y;
                    _path.Cells[i].Type    = RawPath[i].Type;
                    _path.Cells[i].Form    = RawPath[i].Form;
                    _path.Cells[i].Corner  = RawPath[i].Corner;
                    _path.Cells[i].Visible = true;
                    hasSet                 = true;
                }
                else
                {
                    _path.Cells[i].Visible = false;
                }

                _path.Cells[i].MarkDirty();
            }

            // hide path cells, which have no field cell attached
            for (int i = RawPath.Count; i < 4; i++)
            {
                _path.Cells[i].Visible = false;

                _path.Cells[i].MarkDirty();
            }

            _path.MarkDirty();

            return hasSet;
        }

        private bool IsValid(MovementCard card, int i)
        {
            var result = false;
            if (!card.HasUsedFirstType)
            {
                result = (RawPath[i].Type & card.FirstType) != CellType.Empty || card.FirstType == CellType.Common;
            }

            if (!card.HasUsedSecondType)
            {
                result |= (RawPath[i].Type & card.SecondType) != CellType.Empty || card.SecondType == CellType.Common;
            }

            return result;
        }

        public void ClearPaths()
        {
            RawPath.Clear();
            TrySetVisiblePath(null);
        }
    }
}
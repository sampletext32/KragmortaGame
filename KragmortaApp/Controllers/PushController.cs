using System.Collections.Generic;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class PushController : ControllerBase
    {
        public List<AbstractCell> RawPush = new List<AbstractCell>(4);

        private Push _push;

        public PushController(Push push)
        {
            _push = push;
        }

        public bool TryGetCell(int selectedCellX, int selectedCellY, out PushCell pushCell)
        {
            foreach (var cell in _push.Cells)
            {
                if (cell.X == selectedCellX && cell.Y == selectedCellY && cell.Visible)
                {
                    pushCell = cell;
                    return true;
                }
            }

            pushCell = null;
            return false;
        }

        public bool TrySetVisiblePush()
        {
            if (RawPush.Count > 4)
            {
                throw new KragException("Found path cells are supposed to not exceed a count of 4");
            }

            bool hasSet = false;

            // highlight paths
            for (int i = 0; i < RawPush.Count; i++)
            {
                _push.Cells[i].X       = RawPush[i].X;
                _push.Cells[i].Y       = RawPush[i].Y;
                _push.Cells[i].Type    = RawPush[i].Type;
                _push.Cells[i].Visible = true;
                hasSet                 = true;

                _push.Cells[i].MarkDirty();
            }

            // hide path cells, which have no field cell attached
            for (int i = RawPush.Count; i < 4; i++)
            {
                _push.Cells[i].Visible = false;

                _push.Cells[i].MarkDirty();
            }

            _push.MarkDirty();

            return hasSet;
        }

        public void ClearPush()
        {
            RawPush.Clear();
            TrySetVisiblePush();
        }
    }
}
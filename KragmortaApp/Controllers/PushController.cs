using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Cells;
using KragmortaApp.Enums;

namespace KragmortaApp.Controllers
{
    public class PushController : ControllerBase
    {
        public List<AbstractCell> RawPush = new List<AbstractCell>(4);

        private Push _push;

        public PushedStateModel PushedStateModel { get; private set; }

        public PushController(Push push, PushedStateModel pushedStateModel, bool initStates)
        {
            _push            = push;
            PushedStateModel = pushedStateModel;
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
                if (RawPush[i].Type == CellType.Wall)
                {
                    _push.Cells[i].Visible = false;
                    continue;
                }

                _push.Cells[i].X       = RawPush[i].X;
                _push.Cells[i].Y       = RawPush[i].Y;
                _push.Cells[i].Type    = RawPush[i].Type;
                _push.Cells[i].Corner  = RawPush[i].Corner;
                _push.Cells[i].Form    = RawPush[i].Form;
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

        /// <summary>
        /// Excludes the possible
        /// </summary>
        /// <param name="cellX"></param>
        /// <param name="cellY"></param>
        public void Except(int cellX, int cellY)
        {
            for (var i = 0; i < RawPush.Count; i++)
            {
                if (RawPush[i].X == cellX && RawPush[i].Y == cellY)
                {
                    RawPush.RemoveAt(i);
                    return;
                }
            }
        }

        public void ClearPush()
        {
            RawPush.Clear();
            TrySetVisiblePush();
        }

        public void SetVictim(HeroModel victim)
        {
            PushedStateModel.Victim = victim;
        }

        public void ClearVictim()
        {
            PushedStateModel.Victim = null;
        }

        public void SetReturnMoveToPusher(HeroModel pusher)
        {
            PushedStateModel.Pusher                   = pusher;
            PushedStateModel.ShouldReturnMoveToPusher = true;
        }

        public void ClearReturnMoveToPusher()
        {
            PushedStateModel.Pusher                   = null;
            PushedStateModel.ShouldReturnMoveToPusher = false;
        }
    }
}
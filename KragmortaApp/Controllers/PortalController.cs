using System;
using System.Linq;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Cells;
using SFML.System;

namespace KragmortaApp.Controllers
{
    public class PortalController : ControllerBase
    {
        private Portals _portals;
        Random _rand = new Random(DateTime.Now.Millisecond);

        public PortalController(Portals portals, bool initStates)
        {
            _portals = portals;
        }

        /// <summary>
        /// Sets all highlighted portals visible except the one where the current player stands right now.
        /// </summary>
        public void SetAllVisibleExcept(int x, int y)
        {
            for (var i = 0; i < _portals.Cells.Count; i++)
            {
                _portals.Cells[i].Visible = true;
                _portals.Cells[i].MarkDirty();
            }

            _portals.Cells.First(c => c.X == x && c.Y == y).Visible = false;

            _portals.MarkDirty();
        }

        public void SetInvisiblePortals()
        {
            for (var i = 0; i < _portals.Cells.Count; i++)
            {
                _portals.Cells[i].Visible = false;
                _portals.Cells[i].MarkDirty();
            }
        }

        public PortalCell RandomExcept(int cellX, int cellY)
        {
            PortalCell result;
            
            do
            {
                result = _portals.Cells[_rand.Next(_portals.Cells.Count)];
            } while (result.X == cellX && result.Y == cellY);

            return result;
        }
    }
}
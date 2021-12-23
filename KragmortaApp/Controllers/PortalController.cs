using System;
using System.Linq;
using KragmortaApp.Entities;
using SFML.System;

namespace KragmortaApp.Controllers
{
    public class PortalController : ControllerBase
    {
        private Portal _portal;
        Random _rand = new Random(DateTime.Now.Millisecond);

        public PortalController(Portal portal)
        {
            _portal = portal;
        }

        /// <summary>
        /// Sets all highlighted portals visible except the one where the current player stands right now.
        /// </summary>
        /// <param name="x">The abscissa of the cell where the current player stands</param>
        /// <param name="y">The ordinate of the cell where the current player stands</param>
        public void SetVisiblePortals(int x, int y)
        {
            for (var i = 0; i < _portal.Cells.Count; i++)
            {
                _portal.Cells[i].Visible = true;
                _portal.Cells[i].MarkDirty();
            }

            _portal.Cells.First(c => c.X == x && c.Y == y).Visible = false;

            _portal.MarkDirty();
        }

        public void SetInvisiblePortals()
        {
            for (var i = 0; i < _portal.Cells.Count; i++)
            {
                _portal.Cells[i].Visible = false;
                _portal.Cells[i].MarkDirty();
            }
        }

        public PortalCell RandomExcept(int cellX, int cellY)
        {
            PortalCell result;
            
            do
            {
                result = _portal.Cells[_rand.Next(_portal.Cells.Count)];
            } while (result.X == cellX && result.Y == cellY);

            return result;
        }
    }
}
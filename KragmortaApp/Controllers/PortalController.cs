using System.Linq;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class PortalController : ControllerBase
    {
        private Portal _portal;

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
    }
}
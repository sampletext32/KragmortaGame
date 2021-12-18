using System.Collections.Generic;

namespace KragmortaApp.Entities
{
    public class Portal : VisualEntity
    {
        public IReadOnlyList<PortalCell> Cells => _cells;
        private List<PortalCell> _cells;

        public Portal()
        {
            _cells = new List<PortalCell>(7);
            
            // _cells.Add(new PortalCell()
            // {
            //     X       = 0,
            //     Y       = 0,
            //     Visible = true
            // });
            // _cells.Add(new PortalCell()
            // {
            //     X       = 0,
            //     Y       = 0,
            //     Visible = true
            // });
        }
    }
}
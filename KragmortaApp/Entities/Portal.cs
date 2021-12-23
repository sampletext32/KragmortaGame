using System.Collections.Generic;
using SFML.System;

namespace KragmortaApp.Entities
{
    public class Portal : VisualEntity
    {
        private readonly GameField _field;
        public IReadOnlyList<PortalCell> Cells => _cells;
        private List<PortalCell> _cells;

        public Portal(GameField field)
        {
            _field = field;
            _cells = new List<PortalCell>(7);
            
            if (field.SizeX == 10)
            {
                Init10X7();
            }
            else
            {
                Init7X10();
            }
        }

        private void Init7X10()
        {
            var coords = new Vector2i[7];
            coords[0] = new Vector2i(0, 0);
            coords[1] = new Vector2i(6, 0);
            coords[2] = new Vector2i(3, 2);
            coords[3] = new Vector2i(1, 4);
            coords[4] = new Vector2i(5, 4);
            coords[5] = new Vector2i(4, 6);
            coords[6] = new Vector2i(6, 9);
            
            foreach (var coord in coords)
            {
                var cell = _field.GetCell(coord);
                _cells.Add(new PortalCell()
                {
                    X = cell.X,
                    Y = cell.Y,
                    Corner = cell.Corner,
                    Form = cell.Form
                });
            }
        }

        private void Init10X7()
        {
        }
    }
}
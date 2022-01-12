using System.Collections.Generic;
using KragmortaApp.Entities.Cells;
using KragmortaApp.Enums;
using SFML.System;

namespace KragmortaApp.Entities
{
    public class Portals : VisualEntity
    {
        private readonly GameField _field;
        public IReadOnlyList<PortalCell> Cells => _cells;
        private List<PortalCell> _cells;

        public Portals(GameField field)
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
            var cell = _field.GetCell(0, 0);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });
            cell = _field.GetCell(6, 0);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });
            cell = _field.GetCell(3, 2);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });
            cell = _field.GetCell(1, 4);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });
            cell = _field.GetCell(5, 4);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });
            cell = _field.GetCell(4, 6);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });
            cell = _field.GetCell(6, 9);
            _cells.Add(new PortalCell()
            {
                X        = cell.X,
                Y        = cell.Y,
                Corner   = cell.Corner,
                Form     = cell.Form,
                IsPortal = true
            });

            switch (_field.FieldType)
            {
                case FieldType.Mini:
                {
                    _cells.Remove(_cells.Find(c => c.Y == 0));
                    _cells.Remove(_cells.Find(c => c.Y == 0));
                    _cells.Remove(_cells.Find(c => c.Y == 2));

                    break;
                }
                case FieldType.Medium:
                {
                    _cells.Remove(_cells.Find(c => c.Y == 0));
                    _cells.Remove(_cells.Find(c => c.Y == 0));
                    
                    
                    break;
                }
            }
        }

        private void Init10X7()
        {
        }
    }
}
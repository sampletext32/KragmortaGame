using System;
using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class PortalPresenter : CellPresenterAbstract
    {
        private readonly Portals _portals;
        private List<PortalCellDrawable> _drawables;

        public PortalPresenter(Portals portals)
        {
            _portals    = portals;
            _drawables = InitPortalCellDrawables(portals.Cells.Count);

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private List<PortalCellDrawable> InitPortalCellDrawables(int count)
        {
            var result = new List<PortalCellDrawable>(count);

            for (var i = 0; i < count; i++)
            {
                result.Add(new PortalCellDrawable(_portals.Cells[i], CellSize));
            }

            return result;
        }
        

        private void OnFieldOriginChanged(int x, int y)
        {
            for (var i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].ShiftPosition(x, y);
            }
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            for (var i = 0; i < _portals.Cells.Count; i++)
            {
                var positionX = FieldOriginX + (CellSize + CellMargin) * _portals.Cells[i].X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * _portals.Cells[i].Y;
            
                if (x >= positionX &&
                    x < positionX + (CellSize + CellMargin) &&
                    y >= positionY &&
                    y < positionY + (CellSize + CellMargin)
                    && _portals.Cells[i].Visible
                )
                {
                    return true;
                }
            }
            
            return false;
        }

        public override void Render(RenderTarget target)
        {
            if (_portals.Dirty)
            {
                _portals.ClearDirty();
            }
            
            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }

        
    }
}
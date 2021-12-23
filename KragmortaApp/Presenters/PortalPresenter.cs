using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class PortalPresenter : CellPresenterAbstract
    {
        private readonly Portal _portal;
        private List<PortalCellDrawable> _drawables;

        public PortalPresenter(Portal portal)
        {
            _portal    = portal;
            // _drawables = InitPortalCellDrawables(portal.Cells.Count);

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private List<PortalCellDrawable> InitPortalCellDrawables(int count)
        {
            var result = new List<PortalCellDrawable>(count);

            for (var i = 0; i < count; i++)
            {
                // result.Add(new PortalCellDrawable(_portal.Cells[i], CellSize));
            }

            return result;
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            // for (var i = 0; i < _drawables.Count; i++)
            // {
            //     _drawables[i].ShiftPosition(x, y);
            // }
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            // for (var i = 0; i < _portal.Cells.Count; i++)
            // {
            //     var positionX = FieldOriginX + (CellSize + CellMargin) * _portal.Cells[i].X;
            //     var positionY = FieldOriginY + (CellSize + CellMargin) * _portal.Cells[i].Y;
            //
            //     if (x >= positionX &&
            //         x < positionX + (CellSize + CellMargin) &&
            //         y >= positionY &&
            //         y < positionY + (CellSize + CellMargin)
            //         && _portal.Cells[i].Visible
            //     )
            //     {
            //         return true;
            //     }
            // }
            
            return false;
        }

        public override void Render(RenderTarget target)
        {
            // if (_portal.Dirty)
            // {
            //     for (var i = 0; i < _portal.Cells.Count; i++)
            //     {
            //         var positionX = FieldOriginX + (CellSize + CellMargin) * _portal.Cells[i].X;
            //         var positionY = FieldOriginY + (CellSize + CellMargin) * _portal.Cells[i].Y;
            //         _drawables[i].SetPosition(positionX, positionY);
            //     }
            //
            //     _portal.ClearDirty();
            // }
            //
            // foreach (var drawable in _drawables)
            // {
            //     target.Draw(drawable);
            // }
        }
    }
}
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
            _drawables = new List<PortalCellDrawable>(_portal.Cells.Count);
            _drawables.AddRange(portal.Cells.Select(cell =>
            {
                var drawable = new PortalCellDrawable(cell, CellSize);

                var positionX = FieldOriginX + (CellSize + CellMargin);
                var positionY = FieldOriginY + (CellSize + CellMargin);

                drawable.SetPosition(positionX, positionY);
                return drawable;
            }));

            FieldOriginChanged += OnFieldOriginChanged;
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
            for (var i = 0; i < _portal.Cells.Count; i++)
            {
                var positionX = FieldOriginX + (CellSize + CellMargin) * _portal.Cells[i].X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * _portal.Cells[i].Y;

                if (x >= positionX &&
                    x < positionX + (CellSize + CellMargin) &&
                    y >= positionY &&
                    y < positionY + (CellSize + CellMargin)
                    && _portal.Cells[i].Visible
                )
                {
                    return true;
                }
            }

            return false;
        }

        public override void Render(RenderTarget target)
        {
            if (_portal.Dirty)
            {
                for (var i = 0; i < _portal.Cells.Count; i++)
                {
                    var positionX = FieldOriginX + (CellSize + CellMargin) * _portal.Cells[i].X;
                    var positionY = FieldOriginY + (CellSize + CellMargin) * _portal.Cells[i].Y;
                    _drawables[i].SetPosition(positionX, positionY);
                }

                _portal.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }
    }
}
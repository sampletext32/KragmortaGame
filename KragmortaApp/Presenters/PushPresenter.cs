using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class PushPresenter : CellPresenterAbstract
    {
        private readonly Push _push;
        private List<PushCellDrawable> _drawables;
        
        public PushPresenter(Push push)
        {
            _push      = push;
            _drawables = new List<PushCellDrawable>(push.Cells.Count);
            _drawables.AddRange(push.Cells.Select(cell =>
            {
                var drawable = new PushCellDrawable(cell, CellSize);

                var positionX = FieldOriginX + (CellSize + CellMargin) * cell.X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * cell.Y;

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
            for (var i = 0; i < _push.Cells.Count; i++)
            {
                var positionX = FieldOriginX + (CellSize + CellMargin) * _push.Cells[i].X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * _push.Cells[i].Y;

                if (x >= positionX &&
                    x < positionX + (CellSize + CellMargin) &&
                    y >= positionY &&
                    y < positionY + (CellSize + CellMargin)
                    && _push.Cells[i].Visible
                   )
                {
                    return true;
                }
            }

            return false;
        }
        
        public override void Render(RenderTarget target)
        {
            if (_push.Dirty)
            {
                for (var i = 0; i < _push.Cells.Count; i++)
                {
                    var positionX = FieldOriginX + (CellSize + CellMargin) * _push.Cells[i].X;
                    var positionY = FieldOriginY + (CellSize + CellMargin) * _push.Cells[i].Y;
                    _drawables[i].SetPosition(positionX, positionY);
                }

                _push.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }
    }
}
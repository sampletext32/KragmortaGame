using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class PathPresenter : CellPresenterAbstract
    {
        private readonly Path _path;
        private List<PathCellDrawable> _drawables;

        public PathPresenter(Path path)
        {
            _path      = path;
            _drawables = new List<PathCellDrawable>(path.Cells.Count);
            _drawables.AddRange(path.Cells.Select(cell =>
            {
                var drawable = new PathCellDrawable(cell, CellSize);

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
            for (var i = 0; i < _path.Cells.Count; i++)
            {
                var positionX = FieldOriginX + (CellSize + CellMargin) * _path.Cells[i].X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * _path.Cells[i].Y;

                if (x >= positionX &&
                    x < positionX + (CellSize + CellMargin) &&
                    y >= positionY &&
                    y < positionY + (CellSize + CellMargin)
                    && _path.Cells[i].Visible
                   )
                {
                    return true;
                }
            }

            return false;
        }

        public override void Render(RenderTarget target)
        {
            if (_path.Dirty)
            {
                for (var i = 0; i < _path.Cells.Count; i++)
                {
                    var positionX = FieldOriginX + (CellSize + CellMargin) * _path.Cells[i].X;
                    var positionY = FieldOriginY + (CellSize + CellMargin) * _path.Cells[i].Y;
                    _drawables[i].SetPosition(positionX, positionY);
                }

                _path.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }
    }
}
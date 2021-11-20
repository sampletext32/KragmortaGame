using System.Collections.Generic;
using System.Linq;
using MainApp.Drawables;
using MainApp.Entities;
using SFML.Graphics;

namespace MainApp.Presenters
{
    public class PathPresenter : CellPresenterAbstract
    {
        private List<PathCellDrawable> _drawables;

        public PathPresenter(List<PathCell> path)
        {
            _drawables = new List<PathCellDrawable>(path.Count);
            _drawables.AddRange(path.Select(cell =>
            {
                var drawable = new PathCellDrawable(cell, CellSize);

                var positionX = FieldOriginX + (CellSize + CellMargin) * cell.FieldX;
                var positionY = FieldOriginY + (CellSize + CellMargin) * cell.FieldY;

                drawable.SetPosition(positionX, positionY);
                return drawable;
            }));
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return false;
        }

        public override void Render(RenderTarget target)
        {
            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }
    }
}
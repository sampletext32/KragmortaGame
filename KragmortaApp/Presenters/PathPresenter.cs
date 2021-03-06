using System.Collections.Generic;
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

            _drawables = InitPathCellDrawables(path.Cells.Count);
            
            FieldOriginChanged += OnFieldOriginChanged;
        }

        private List<PathCellDrawable> InitPathCellDrawables(int count)
        {
            var result = new List<PathCellDrawable>(count);

            for (var i = 0; i < count; i++)
            {
                result.Add(new PathCellDrawable(_path.Cells[i], CellSize));
            }

            return result;
        }
        
        private void LoadTextures()
        {
            for (var i = 0; i < _drawables.Count; i++)
            {
                if (_path.Cells[i].Visible)
                {
                    _drawables[i].LoadTexture();
                }
            }
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
                LoadTextures();
                
                _path.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }

        
    }
}
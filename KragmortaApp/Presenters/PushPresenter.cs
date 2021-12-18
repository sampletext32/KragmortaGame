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
            
            _drawables = InitPushCellDrawables(push.Cells.Count);

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private List<PushCellDrawable> InitPushCellDrawables(int count)
        {
            var result = new List<PushCellDrawable>(count);

            for (var i = 0; i < count; i++)
            {
                result.Add(new PushCellDrawable(_push.Cells[i], CellSize));
            }

            return result;
        }
        
        private void LoadTextures()
        {
            for (var i = 0; i < _drawables.Count; i++)
            {
                if (_push.Cells[i].Visible)
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
                LoadTextures();

                _push.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
            }
        }
    }
}
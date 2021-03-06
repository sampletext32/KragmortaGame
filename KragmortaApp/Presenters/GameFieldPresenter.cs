using System.Collections.Generic;
using KragmortaApp.Drawables.FieldCellDrawables;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class GameFieldPresenter : CellPresenterAbstract
    {
        private readonly GameField _field;

        private List<TexturedFieldCellDrawable> _drawables;

        public GameFieldPresenter(GameField field)
        {
            _field     = field;
            _drawables = InitTexturedFieldCellDrawables(field.Cells.Count);

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            for (var i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].ShiftPosition(x, y);
            }
        }

        private List<TexturedFieldCellDrawable> InitTexturedFieldCellDrawables(int count)
        {
            var result = new List<TexturedFieldCellDrawable>(count);

            for (var i = 0; i < count; i++)
            {
                result.Add(new TexturedFieldCellDrawable(_field.Cells[i], CellSize));
            }

            return result;
        }
        public override void Render(RenderTarget target)
        {
            if (_field.Dirty)
            {
                _field.ClearDirty();
            }

            foreach (var drawable in _drawables)
            {
                if (drawable.Cell.Visible) target.Draw(drawable);
            }
        }

        /// <summary>
        /// Ensures that x and y coordinates are within the presented game field
        /// </summary>
        public override bool IsMouseWithinBounds(int x, int y)
        {
            return !(x < FieldOriginX ||
                     x >= FieldOriginX + (CellSize + CellMargin) * _field.SizeX ||
                     y < FieldOriginY ||
                     y >= FieldOriginY + (CellSize + CellMargin) * _field.SizeY
                );
        }

        public bool TryConvertMouseCoordsToCellCoords(int x, int y, out int cellX, out int cellY)
        {
            foreach (var drawable in _drawables)
            {
                if (drawable.IsMouseWithinBounds(x, y))
                {
                    if (drawable.IsTransparentPixel(x, y)) continue;

                    cellX = drawable.Cell.X;
                    cellY = drawable.Cell.Y;

                    return true;
                }
            }

            cellX = cellY = -1;
            return false;
        }
    }
}
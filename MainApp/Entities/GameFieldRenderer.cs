using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities
{
    public class GameFieldRenderer
    {
        private readonly GameField _field;
        private List<FieldCellDrawable> _drawables;

        public GameFieldRenderer(GameField field)
        {
            _field     = field;
            _drawables = new List<FieldCellDrawable>(field.Cells.Count);
            _drawables.AddRange(field.Cells.Select(cell =>
            {
                var drawable = new FieldCellDrawable(field.CellSize);
                drawable.SetOutlineThickness(field.CellMargin);

                var positionX = (_field.CellSize + _field.CellMargin) * cell.X;
                var positionY = (_field.CellSize + _field.CellMargin) * cell.Y;

                drawable.SetPosition(positionX, positionY);
                return drawable;
            }));
        }

        public void Render(RenderTarget target)
        {
            for (var i = 0; i < _field.Cells.Count; i++)
            {
                _drawables[i].SetOutlineVisible(_field.Cells[i].Hovered);
                _drawables[i].SetFlagsVisibility(_field.Cells[i].Type);
                
                target.Draw(_drawables[i]);
            }
        }
    }
}
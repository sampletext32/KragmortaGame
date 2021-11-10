using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities
{
    public class GameFieldPresenter
    {
        private readonly GameField _field;
        private List<FieldCellDrawable> _drawables;

        public int CellSize = 96;
        public int CellMargin = 6;

        public GameFieldPresenter(GameField field)
        {
            _field     = field;
            _drawables = new List<FieldCellDrawable>(field.Cells.Count);
            _drawables.AddRange(field.Cells.Select(cell =>
            {
                var drawable = new FieldCellDrawable(CellSize);
                drawable.SetOutlineThickness(CellMargin);

                var positionX = (CellSize + CellMargin) * cell.X;
                var positionY = (CellSize + CellMargin) * cell.Y;

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
                _drawables[i].SetSelected(_field.Cells[i].Selected);
                
                target.Draw(_drawables[i]);
            }
        }

        public bool IsMouseWithinBounds(int x, int y)
        {
            return !(x >= (CellSize + CellMargin) * _field.SizeX ||
                   y >= (CellSize + CellMargin) * _field.SizeY);
        }

        public int ConvertMouseXToCellX(int x)
        {
            return x / (CellSize + CellMargin);
        }
        public int ConvertMouseYToCellY(int y)
        {
            return y / (CellSize + CellMargin);
        }
    }
}
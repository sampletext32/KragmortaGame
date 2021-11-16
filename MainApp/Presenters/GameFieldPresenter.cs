using System.Collections.Generic;
using System.Linq;
using MainApp.Entities;
using MainApp.Models;
using SFML.Graphics;

namespace MainApp.Presenters
{
    public class GameFieldPresenter : CellPresenterAbstract
    {
        private readonly GameField _field;
        private List<FieldCellDrawable> _drawables;

        public GameFieldPresenter(GameField field)
        {
            _field     = field;
            _drawables = new List<FieldCellDrawable>(field.Cells.Count);
            _drawables.AddRange(field.Cells.Select(cell =>
            {
                var drawable = new FieldCellDrawable(CellSize);
                drawable.SetOutlineThickness(CellMargin);

                var positionX = FieldOriginX + (CellSize + CellMargin) * cell.X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * cell.Y;

                drawable.SetPosition(positionX, positionY);
                return drawable;
            }));
        }

        public override void Render(RenderTarget target)
        {
            for (var i = 0; i < _field.Cells.Count; i++)
            {
                _drawables[i].SetOutlineVisible(_field.Cells[i].Hovered);
                _drawables[i].SetFlagsVisibility(_field.Cells[i].Type);
                _drawables[i].SetClicked(_field.Cells[i].Clicked);

                target.Draw(_drawables[i]);
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
    }
}
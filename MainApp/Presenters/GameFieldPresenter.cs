using System.Collections.Generic;
using System.Linq;
using MainApp.Drawables;
using MainApp.Entities;
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
                var drawable = new FieldCellDrawable(cell, CellSize);

                var positionX = FieldOriginX + (CellSize + CellMargin) * cell.X;
                var positionY = FieldOriginY + (CellSize + CellMargin) * cell.Y;

                drawable.SetPosition(positionX, positionY);
                drawable.SetOutlineThickness(CellMargin);
                return drawable;
            }));
        }
        
        public override void Render(RenderTarget target)
        {
            foreach (var drawable in _drawables)
            {
                target.Draw(drawable);
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
using System.Collections.Generic;
using System.Linq;
using MainApp.Entities.Models;
using SFML.Graphics;

namespace MainApp.Entities.Presenters
{
    public class GameFieldPresenter
    {
        private readonly GameField _field;
        private List<FieldCellDrawable> _drawables;

        public readonly int CellSize = 96;
        public readonly int CellMargin = 6;

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
                _drawables[i].SetClicked(_field.Cells[i].Clicked);
                
                target.Draw(_drawables[i]);
            }
        }

        /// <summary>
        /// Ensures that x and y coordinates are within the presented game field
        /// </summary>
        public bool IsMouseWithinBounds(int x, int y)
        {
            // TODO: Add offset of the field (currently at 0,0)
            return !(x >= (CellSize + CellMargin) * _field.SizeX ||
                   y >= (CellSize + CellMargin) * _field.SizeY);
        }

        /// <summary>
        /// Converts the screen X to field Cell X
        /// </summary>
        public int ConvertMouseXToCellX(int x)
        {
            return x / (CellSize + CellMargin);
        }
        
        /// <summary>
        /// Converts the screen Y to field Cell Y
        /// </summary>
        public int ConvertMouseYToCellY(int y)
        {
            return y / (CellSize + CellMargin);
        }
    }
}
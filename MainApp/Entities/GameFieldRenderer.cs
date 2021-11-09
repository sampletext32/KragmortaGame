using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities
{
    public class GameFieldRenderer
    {
        private readonly GameField _field;
        private RectangleShape _shape;

        public GameFieldRenderer(GameField field)
        {
            _field = field;
            _shape = new RectangleShape()
            {
                Size      = new Vector2f(field.CellSize, field.CellSize),
                FillColor = Color.Red
            };
        }

        public void Render(RenderTarget target)
        {
            for (var i = 0; i < _field.Cells.Count; i++)
            {
                if (_field.Cells[i].Hovered)
                {
                    _shape.OutlineColor     = Color.White;
                    _shape.OutlineThickness = _field.CellMargin;
                }
                else
                {
                    _shape.OutlineColor     = Color.Transparent;
                    _shape.OutlineThickness = 0;
                }

                var positionX = (_field.CellSize + _field.CellMargin) * _field.Cells[i].X;
                var positionY = (_field.CellSize + _field.CellMargin) * _field.Cells[i].Y;
                _shape.Position = new Vector2f(positionX, positionY);
                target.Draw(_shape);
            }
        }
    }
}
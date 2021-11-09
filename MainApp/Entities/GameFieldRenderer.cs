using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities
{
    public class GameFieldRenderer
    {
        public void Render(GameField field, RenderTarget target)
        {
            int cellSize = 128;

            RectangleShape shape = new RectangleShape()
            {
                Size      = new Vector2f(cellSize, cellSize),
                FillColor = Color.Red
            };

            int margin = 6;

            for (var i = 0; i < field.Cells.Count; i++)
            {
                shape.Position = new Vector2f((cellSize + margin) * field.Cells[i].X, (cellSize + margin) * field.Cells[i].Y);
                target.Draw(shape);
            }
        }
    }
}
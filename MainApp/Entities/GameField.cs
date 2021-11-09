using System.Collections.Generic;
using SFML.Graphics;

namespace MainApp.Entities
{
    public class GameField
    {
        public readonly List<FieldCell> Cells;

        private GameFieldRenderer _renderer;

        public GameField(int sizeX, int sizeY)
        {
            Cells = new List<FieldCell>(sizeX * sizeY);
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    Cells.Add(
                        new FieldCell()
                        {
                            X = i,
                            Y = j
                        }
                    );
                }
            }

            _renderer = new GameFieldRenderer();
        }

        public void OnRender(RenderTarget target)
        {
            _renderer.Render(this, target);
        }
    }
}
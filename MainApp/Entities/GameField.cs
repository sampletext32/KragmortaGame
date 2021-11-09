using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace MainApp.Entities
{
    public class GameField
    {
        private readonly int _sizeX;
        private readonly int _sizeY;

        public int CellSize = 96;
        public int CellMargin = 6;
        public readonly List<FieldCell> Cells;

        private GameFieldRenderer _renderer;

        public GameField(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            Cells  = new List<FieldCell>(sizeX * sizeY);
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

            _renderer = new GameFieldRenderer(this);
        }

        public void OnRender(RenderTarget target)
        {
            _renderer.Render(target);
        }

        public void OnMouseMoved(int x, int y)
        {
            if (x > (CellSize + CellMargin) * _sizeX ||
                y > (CellSize + CellMargin) * _sizeY)
            {
                Console.WriteLine("Hit no cell");
                return;
            }
            int cX = x / (CellSize + CellMargin);
            int cY = y / (CellSize + CellMargin);
            
            for (var i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].X == cX && Cells[i].Y == cY)
                {
                    Cells[i].Hovered = true;
                }
                else
                {
                    Cells[i].Hovered = false;
                }
            }
        }
    }
}
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

        private FieldCell _lastMouseOverCell = null;

        private readonly List<FieldCell> _cells;
        public IReadOnlyList<FieldCell> Cells => _cells;

        private GameFieldRenderer _renderer;

        public GameField(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _cells = new List<FieldCell>(sizeX * sizeY);
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    _cells.Add(
                        new FieldCell()
                        {
                            X    = i,
                            Y    = j,
                            Type = (FieldType)(1 << random.Next(4))
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
            if (x >= (CellSize + CellMargin) * _sizeX ||
                y >= (CellSize + CellMargin) * _sizeY)
            {
                if (_lastMouseOverCell is not null)
                {
                    _lastMouseOverCell.Hovered = false;
                    _lastMouseOverCell         = null;
                }

                return;
            }

            int cX = x / (CellSize + CellMargin);
            int cY = y / (CellSize + CellMargin);

            if (_lastMouseOverCell is not null)
            {
                _lastMouseOverCell.Hovered = false;
            }

            // Don't ask why this formula works, because it just works
            _lastMouseOverCell         = Cells[cY + cX * _sizeY];
            _lastMouseOverCell.Hovered = true;
        }
    }
}
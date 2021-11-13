using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace MainApp.Entities
{
    public class GameField
    {
        public readonly int SizeX;
        public readonly int SizeY;
        public IReadOnlyList<FieldCell> Cells => _cells;

        private FieldCell _lastMouseOverCell = null;
        private FieldCell _lastMouseDownOverCell = null;

        private readonly List<FieldCell> _cells;

        private GameFieldPresenter _fieldPresenter;

        public GameField(int sizeX, int sizeY)
        {
            SizeX  = sizeX;
            SizeY  = sizeY;
            _cells = new List<FieldCell>(sizeX * sizeY);
            var random = new Random(DateTime.Now.Millisecond);

            // Here we write cells with row-first order, so that addressing is correct
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    _cells.Add(
                        new FieldCell()
                        {
                            X    = j,
                            Y    = i,
                            Type = (FieldType)(1 << random.Next(5))
                        }
                    );
                }
            }

            _fieldPresenter = new GameFieldPresenter(this);
        }

        /// <summary>
        /// Retrieves field cell by it's field coordinates
        /// </summary>
        private FieldCell GetCell(int cX, int cY)
        {
            return _cells[cX + cY * SizeX];
        }

        public void OnRender(RenderTarget target)
        {
            _fieldPresenter.Render(target);
        }

        public void OnMouseMoved(int x, int y)
        {
            if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                if (_lastMouseOverCell is not null)
                {
                    _lastMouseOverCell.Hovered = false;
                    _lastMouseOverCell         = null;
                }

                return;
            }

            int cX = _fieldPresenter.ConvertMouseXToCellX(x);
            int cY = _fieldPresenter.ConvertMouseYToCellY(y);

            if (_lastMouseOverCell is not null)
            {
                _lastMouseOverCell.Hovered = false;
            }

            var fieldCell = GetCell(cX, cY);
            fieldCell.Hovered  = true;
            _lastMouseOverCell = fieldCell;
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                return;
            }

            var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            var cY = _fieldPresenter.ConvertMouseYToCellY(y);

            var fieldCell = GetCell(cX, cY);
            fieldCell.Clicked      = true;
            _lastMouseDownOverCell = fieldCell;
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            // this small check is needed for mouse release not over initially clicked cell (click and drag behavior)
            if (_lastMouseDownOverCell is not null)
            {
                _lastMouseDownOverCell.Clicked = false;
                _lastMouseDownOverCell         = null;
            }

            if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                return;
            }

            var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            var cY = _fieldPresenter.ConvertMouseYToCellY(y);

            var fieldCell = GetCell(cX, cY);
            fieldCell.Clicked = false;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace MainApp.Entities
{
    public class GameField
    {
        public readonly int SizeX;
        public readonly int SizeY;

        private FieldCell _lastMouseOverCell = null;
        private FieldCell _lastMouseDownOverCell = null;

        private readonly List<FieldCell> _cells;
        public IReadOnlyList<FieldCell> Cells => _cells;

        private GameFieldPresenter _presenter;

        public GameField(int sizeX, int sizeY)
        {
            SizeX  = sizeX;
            SizeY  = sizeY;
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

            _presenter = new GameFieldPresenter(this);
        }

        public void OnRender(RenderTarget target)
        {
            _presenter.Render(target);
        }

        public void OnMouseMoved(int x, int y)
        {
            if (!_presenter.IsMouseWithinBounds(x, y))
            {
                if (_lastMouseOverCell is not null)
                {
                    _lastMouseOverCell.Hovered = false;
                    _lastMouseOverCell         = null;
                }

                return;
            }

            int cX = _presenter.ConvertMouseXToCellX(x);
            int cY = _presenter.ConvertMouseYToCellY(y);

            if (_lastMouseOverCell is not null)
            {
                _lastMouseOverCell.Hovered = false;
            }

            // Don't ask why this formula works, because it just works
            _lastMouseOverCell         = Cells[cY + cX * SizeY];
            _lastMouseOverCell.Hovered = true;
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y))
            {
                return;
            }

            var cX = _presenter.ConvertMouseXToCellX(x);
            var cY = _presenter.ConvertMouseYToCellY(y);

            var fieldCell = Cells[cY + cX * SizeY];
            fieldCell.Selected     = true;
            _lastMouseDownOverCell = fieldCell;
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            if (_lastMouseDownOverCell is not null)
            {
                _lastMouseDownOverCell.Selected = false;
                _lastMouseDownOverCell          = null;
            }

            if (!_presenter.IsMouseWithinBounds(x, y))
            {
                return;
            }

            var cX = _presenter.ConvertMouseXToCellX(x);
            var cY = _presenter.ConvertMouseYToCellY(y);

            var fieldCell = Cells[cY + cX * SizeY];
            fieldCell.Selected = false;
        }
    }
}
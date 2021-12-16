using System;
using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class GameFieldLayer : AbstractLayer
    {
        private readonly GameFieldPresenter _presenter;
        private readonly GameFieldHandler _handler;

        public GameFieldLayer(GameFieldPresenter presenter, GameFieldHandler handler, string title = "GameField Layer") : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler   = handler;
        }

        public override bool TryHandleMouseMoved(int x, int y)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            if (!_presenter.TryConvertMouseCoordsToCellCoords(x, y, out int cellX, out int cellY)) return false;

            _handler.OnMouseMovedOverCell(cellX, cellY);
            return true;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            if (!_presenter.TryConvertMouseCoordsToCellCoords(x, y, out int cellX, out int cellY)) return false;

            _handler.OnMousePressedCell(cellX, cellY);

            return true;
        }

        public override void HandleMouseLeft()
        {
            _handler.OnMouseLeft();
        }

        public override bool TryHandleMouseScroll(int x, int y, bool isVertical, float delta)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            if (isVertical)
            {
                if (Environment.OSVersion.Platform == PlatformID.MacOSX)
                {
                    CellPresenterAbstract.FieldOriginY -= (int)(10 * delta);
                    CellPresenterAbstract.InvokeFieldOriginChanged(0, -(int)(10 * delta));
                }
                else
                {
                    CellPresenterAbstract.FieldOriginY += (int)(10 * delta);
                    CellPresenterAbstract.InvokeFieldOriginChanged(0, (int)(10 * delta));
                }
            }
            else
            {
                CellPresenterAbstract.FieldOriginX -= (int)(10 * delta);
                CellPresenterAbstract.InvokeFieldOriginChanged(-(int)(10 * delta), 0);
            }

            return true;
        }
    }
}
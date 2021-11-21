using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
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

            var cellX = _presenter.ConvertMouseXToCellX(x);
            var cellY = _presenter.ConvertMouseYToCellY(y);

            _handler.OnMouseMovedOverCell(cellX, cellY);
            return true;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            var cellX = _presenter.ConvertMouseXToCellX(x);
            var cellY = _presenter.ConvertMouseYToCellY(y);

            _handler.OnMousePressedCell(cellX, cellY);

            return true;
        }

        public override void HandleMouseLeft()
        {
            _handler.OnMouseLeft();
        }
    }
}
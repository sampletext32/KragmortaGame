using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class MovementDeckLayer : AbstractLayer
    {
        private readonly MovementDeckPresenter _presenter;
        private readonly MovementDeckHandler _handler;

        public MovementDeckLayer(MovementDeckPresenter presenter, MovementDeckHandler handler, string title = "MovementDeck layer") : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler   = handler;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            if (_presenter.TryGetCardFromMousePosition(x, y, out var card))
            {
                _handler.OnCardPressed(card);
            }

            return true;
        }
    }
}
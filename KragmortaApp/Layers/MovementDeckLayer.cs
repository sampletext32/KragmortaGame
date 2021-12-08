using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class MovementDeckLayer : AbstractLayer
    {
        private readonly MovementDecksPresenter _presenter;
        private readonly MovementDeckHandler _handler;

        public MovementDeckLayer(MovementDecksPresenter presenter, MovementDeckHandler handler, string title = "MovementDeck layer") : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler   = handler;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            if (_presenter.TryGetCardFromMousePosition(x, y, out var card))
            {
                if (mouseButton == KragMouseButton.Left)
                {
                    _handler.OnCardPressed(card);
                }
                else
                {
                    _handler.ContextMenuFor(card, x, y);
                }
            }

            return true;
        }
    }
}
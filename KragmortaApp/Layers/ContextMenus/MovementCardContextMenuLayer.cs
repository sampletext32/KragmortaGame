using KragmortaApp.Handlers.ContextMenus;
using KragmortaApp.Presenters.ContextMenus;

namespace KragmortaApp.Layers.ContextMenus
{
    public class MovementCardContextMenuLayer : AbstractLayer
    {
        private readonly MovementCardContextMenuPresenter _presenter;
        private readonly MovementCardContextMenuHandler _handler;

        public MovementCardContextMenuLayer(
            MovementCardContextMenuPresenter presenter,
            MovementCardContextMenuHandler handler,
            string title = nameof(MovementCardContextMenuLayer)
        ) : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler   = handler;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (_presenter.IsMouseWithinBounds(x, y))
            {
                if (_presenter.IsMouseOverContextMenu(x, y))
                {
                    // Handle click on context menu
                }
                else
                {
                    _handler.Dismiss();
                    // Dismiss context menu
                }

                return true;
            }

            return false;
        }
    }
}
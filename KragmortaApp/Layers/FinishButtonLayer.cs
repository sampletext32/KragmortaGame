using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class FinishButtonLayer : AbstractLayer
    {
        private readonly FinishButtonPresenter _presenter;
        private readonly FinishButtonHandler _handler;

        public FinishButtonLayer(FinishButtonPresenter presenter, FinishButtonHandler handler, string title = nameof(FinishButtonLayer)) : base(presenter, handler, title)
        {
            _presenter    = presenter;
            _handler = handler;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (_presenter.IsMouseWithinBounds(x, y))
            {
                _handler.FinishTurn();
            }

            return false;
        }
    }
}
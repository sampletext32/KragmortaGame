using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class PushLayer : AbstractLayer
    {
        private readonly PushPresenter _presenter;
        private readonly PushHandler _handler;
        
        public PushLayer(PushPresenter presenter, PushHandler handler, string title = "Push layer") : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler   = handler;
        }
        
        
        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            var selectedCellX = _presenter.ConvertMouseXToCellX(x);
            var selectedCellY = _presenter.ConvertMouseYToCellY(y);

            _handler.OnPushCellClicked(selectedCellX, selectedCellY, mouseButton);

            return true;
        }
    }
}
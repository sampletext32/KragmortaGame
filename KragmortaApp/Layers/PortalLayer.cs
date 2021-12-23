using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class PortalLayer : AbstractLayer
    {
        private PortalPresenter _presenter;
        private PortalHandler _handler;
        
        public PortalLayer(PortalPresenter presenter, PortalHandler handler, string title = "Portal layer") : base(presenter, handler, title)
        {
            _presenter    = presenter;
            _handler = handler;
        }
        
        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            var selectedCellX = _presenter.ConvertMouseXToCellX(x);
            var selectedCellY = _presenter.ConvertMouseYToCellY(y);

            _handler.OnPortalCellClicked(selectedCellX, selectedCellY, mouseButton);

            return true;
        }
    }
}
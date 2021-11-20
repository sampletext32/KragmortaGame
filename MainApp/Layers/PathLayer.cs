using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public class PathLayer: Layer
    {
        private readonly PathPresenter _presenter;
        private readonly PathHandler _handler;

        public PathLayer(PathPresenter presenter, PathHandler handler, string title = "Path layer") : base(presenter, handler, title)
        {
            _presenter    = presenter;
            _handler = handler;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            var selectedCellX = _presenter.ConvertMouseXToCellX(x);
            var selectedCellY = _presenter.ConvertMouseYToCellY(y);
            
            _handler.OnMousePressed(selectedCellX, selectedCellY, mouseButton);
        }
    }
}
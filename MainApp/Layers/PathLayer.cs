using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public class PathLayer: AbstractLayer
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
            
            _handler.OnCellClicked(selectedCellX, selectedCellY, mouseButton);

            return true;
        }

        public override void HandleMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            base.HandleMouseReleased(x, y, mouseButton);
        }
    }
}
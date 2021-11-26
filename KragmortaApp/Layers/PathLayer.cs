using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class PathLayer : AbstractLayer
    {
        private readonly PathPresenter _presenter;
        private readonly PathHandler _handler;

        public PathLayer(PathPresenter presenter, PathHandler handler, string title = "Path layer") : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler   = handler;
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            var selectedCellX = _presenter.ConvertMouseXToCellX(x);
            var selectedCellY = _presenter.ConvertMouseYToCellY(y);

            _handler.OnPathCellClicked(selectedCellX, selectedCellY, mouseButton);

            return true;
        }
    }
}
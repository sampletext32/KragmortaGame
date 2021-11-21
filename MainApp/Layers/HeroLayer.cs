using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public class HeroLayer : AbstractLayer
    {
        private readonly HeroPresenter _presenter;
        private readonly HeroHandler _handler;

        public HeroLayer(HeroPresenter presenter, HeroHandler handler, string title = "Hero layer") : base(presenter, handler, title)
        {
            _presenter    = presenter;
            _handler = handler;
        }

        public override void HandleMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            base.HandleMouseReleased(x, y, mouseButton);
        }

        public override bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            return base.TryHandleMousePressed(x, y, mouseButton);
        }
    }
}
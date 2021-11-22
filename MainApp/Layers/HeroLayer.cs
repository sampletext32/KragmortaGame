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
    }
}
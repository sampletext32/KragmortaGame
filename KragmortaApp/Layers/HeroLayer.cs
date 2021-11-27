using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
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
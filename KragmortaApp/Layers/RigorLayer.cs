using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class RigorLayer : AbstractLayer
    {
        private readonly RigorPresenter _presenter;
        private readonly RigorHandler _handler;

        public RigorLayer(RigorPresenter presenter, RigorHandler handler, string title = "Rigor layer") : base(presenter, handler, title)
        {
            _presenter = presenter;
            _handler        = handler;
        }
    }
}
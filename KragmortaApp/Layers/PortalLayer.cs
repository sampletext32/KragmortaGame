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
    }
}
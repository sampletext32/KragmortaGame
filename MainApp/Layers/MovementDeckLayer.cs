using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public class MovementDeckLayer : AbstractLayer
    {
        private readonly MovementDeckPresenter _presenter;
        private readonly MovementDeckHandler _handler;

        public MovementDeckLayer(MovementDeckPresenter presenter, MovementDeckHandler handler, string title = "MovementDeck layer") : base(presenter, handler, title)
        {
            _presenter    = presenter;
            _handler = handler;
        }
    }
}
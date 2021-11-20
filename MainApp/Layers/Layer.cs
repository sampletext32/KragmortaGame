using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public abstract class Layer
    {
        private Presenter _presenter;
        private Handler _handler;

        protected Layer(Presenter presenter, Handler handler)
        {
            _presenter    = presenter;
            _handler = handler;
        }
    }
}
using MainApp.Handlers;
using MainApp.Presenters;
using SFML.Graphics;

namespace MainApp.Layers
{
    public class AbstractLayer
    {
        private readonly Presenter _presenter;
        private readonly AbstractHandler _handler;
        
        public string Title { get; private set; }

        public AbstractLayer(Presenter presenter, AbstractHandler handler, string title = "Untitled layer")
        {
            _presenter = presenter;
            _handler   = handler;
            Title      = title;
        }

        public void Render(RenderTarget target)
        {
            _presenter.Render(target);
        }

        public virtual bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            _handler.OnMousePressed(x, y, mouseButton);
            return true;
        }

        public virtual void HandleMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _handler.OnMouseReleased(x, y, mouseButton);
        }

        public virtual bool TryHandleMouseMoved(int x, int y)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            _handler.OnMouseMoved(x, y);
            return true;
        }

        public void HandleMouseLeft()
        {
            _handler.OnMouseLeft();
        }

        public void HandleWindowResized(int width, int height)
        {
            _presenter.OnWindowResized(width, height);
        }
    }
}
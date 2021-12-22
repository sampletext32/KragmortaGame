using KragmortaApp.Handlers;
using KragmortaApp.Presenters;
using SFML.Graphics;
using SFML.Window;

namespace KragmortaApp.Layers
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

        /// <summary>
        /// Handles Mouse Press Event
        /// <remarks>
        /// X and Y are NOT supposed to be inside the layer
        /// </remarks>
        /// </summary>
        public virtual bool TryHandleMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            _handler.RawOnMousePressed(x, y, mouseButton);
            return true;
        }

        /// <summary>
        /// Handles Mouse Release Event
        /// <remarks>
        /// X and Y are NOT supposed to be inside the layer
        /// </remarks>
        /// </summary>
        public virtual void HandleMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _handler.RawOnMouseReleased(x, y, mouseButton);
        }

        /// <summary>
        /// Handles Mouse Move Event
        /// <remarks>
        /// X and Y are NOT supposed to be inside the layer
        /// </remarks>
        /// </summary>
        public virtual bool TryHandleMouseMoved(int x, int y)
        {
            if (!_presenter.IsMouseWithinBounds(x, y)) return false;

            _handler.RawOnMouseMoved(x, y);
            return true;
        }

        public virtual void HandleMouseLeft()
        {
            _handler.RawOnMouseLeft();
        }

        public void HandleWindowResized(int width, int height)
        {
            _presenter.OnWindowResized(width, height);
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual bool TryHandleMouseScroll(int x, int y, bool isVertical, float delta)
        {
            return false;
        }

        public virtual bool TryHandleKeyPressed(Keyboard.Key code)
        {
            return false;
        }
    }
}
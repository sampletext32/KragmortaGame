using MainApp.Controllers;

namespace MainApp.Handlers
{
    public abstract class AbstractHandler
    {
        private ControllerBase _controller;

        protected AbstractHandler(ControllerBase controller)
        {
            _controller = controller;
        }

        public virtual void RawOnMouseMoved(int x, int y)
        {
        }

        public virtual void RawOnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
        }

        public virtual void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public virtual void RawOnMouseLeft()
        {
        }
    }
}
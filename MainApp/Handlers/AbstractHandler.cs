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

        public abstract void OnMouseMoved(int x, int y);
        public abstract void OnMousePressed(int x, int y, KragMouseButton mouseButton);
        public abstract void OnMouseReleased(int x, int y, KragMouseButton mouseButton);
        public abstract void OnMouseLeft();
    }
}
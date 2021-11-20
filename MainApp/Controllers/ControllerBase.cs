namespace MainApp.Controllers
{
    public abstract class ControllerBase
    {
        public abstract void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton);

        public abstract void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton);

        public abstract void OnMouseMoved(int x, int y);
    }
}
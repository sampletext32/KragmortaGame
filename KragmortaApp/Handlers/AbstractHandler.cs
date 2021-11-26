using KragmortaApp.Controllers;

namespace KragmortaApp.Handlers
{
    public abstract class AbstractHandler
    {
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
using SFML.Graphics;

namespace MainApp.Entities.Presenters
{
    public abstract class Presenter
    {
        public abstract bool IsMouseWithinBounds(int x, int y);

        public virtual void OnWindowResized(int width, int height)
        {
        }

        public abstract void Render(RenderTarget target);
    }
}
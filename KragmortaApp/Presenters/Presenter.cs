using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public abstract class Presenter
    {
        /// <summary>
        /// Determines, whether the given x and y are inside the presented object
        /// </summary>
        public abstract bool IsMouseWithinBounds(int x, int y);

        public virtual void OnWindowResized(int width, int height)
        {
        }

        public abstract void Render(RenderTarget target);
    }
}
using SFML.Graphics;

namespace MainApp.Scenes
{
    public abstract class Scene
    {
        public abstract void OnCreate();

        public abstract void OnUpdate(float deltaTime);

        public abstract void OnRender(RenderTarget renderTarget);

        public abstract void OnMouseMoved(int x, int y);
        public abstract void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton);
        public abstract void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton);
    }
}
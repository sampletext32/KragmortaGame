using SFML.Graphics;

namespace MainApp.Scenes
{
    public abstract class Scene
    {
        public abstract void OnCreate();

        public abstract void OnUpdate(float deltaTime);

        public abstract void OnRender(RenderTarget renderTarget);
    }
}
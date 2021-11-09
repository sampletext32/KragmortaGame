using MainApp.Entities;
using SFML.Graphics;

namespace MainApp.Scenes
{
    public class GameTableScene : Scene
    {
        private GameField _field;

        public override void OnCreate()
        {
            _field = new GameField(10, 7);
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget renderTarget)
        {
            _field.OnRender(renderTarget);
        }
    }
}
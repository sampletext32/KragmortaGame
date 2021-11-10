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

        public override void OnMouseMoved(int x, int y)
        {
            _field.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _field.OnMouseButtonPressed(x, y, mouseButton);
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _field.OnMouseButtonReleased(x, y, mouseButton);
        }
    }
}
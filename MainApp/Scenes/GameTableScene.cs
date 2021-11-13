using MainApp.Entities;
using SFML.Graphics;

namespace MainApp.Scenes
{
    public class GameTableScene : Scene
    {
        private GameField _field;

        private Profile _profile;

        private ProfilePresenter _profilePresenter;

        public override void OnCreate()
        {
            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Sample Text"
            };

            _profilePresenter = new ProfilePresenter(_profile, Corner.TopRight);
        }
        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            _field.OnRender(target);

            _profilePresenter.Render(target);
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

        public override void OnWindowResized(int width, int height)
        {
            _profilePresenter.OnWindowResized(width, height);
        }
    }
}
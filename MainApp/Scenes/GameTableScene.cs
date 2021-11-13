using MainApp.Entities;
using SFML.Graphics;

namespace MainApp.Scenes
{
    public class GameTableScene : Scene
    {
        private GameField _field;

        private Profile _profile;
        private HeroModel _hero;

        private ProfilePresenter _profilePresenter;
        private HeroPresenter _heroPresenter;
        private HeroController _heroController;

        public override void OnCreate()
        {
            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };

            _profilePresenter = new ProfilePresenter(_profile, Corner.TopRight);

            _hero = new HeroModel("Spidar Woman", 3, 5);
            _heroPresenter = new HeroPresenter(_hero);
            _heroController = new HeroController(_hero, _heroPresenter);

            _hero.FieldY = 2;
            // _heroPresenter.OnHeroMoved();
        }
        public override void OnUpdate(float deltaTime)
        {
            
        }

        public override void OnRender(RenderTarget target)
        {
            _field.OnRender(target);

            _profilePresenter.Render(target);
            _heroPresenter.Render(target);
        }

        public override void OnMouseMoved(int x, int y)
        {
            _field.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _field.OnMouseButtonPressed(x, y, mouseButton);
            _heroController.OnMouseButtonPressed(x, y, mouseButton);
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
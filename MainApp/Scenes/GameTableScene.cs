using MainApp.Entities;
using MainApp.Entities.Controllers;
using MainApp.Entities.Enums;
using MainApp.Entities.Models;
using MainApp.Entities.Presenters;
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
        private GameFieldPresenter _fieldPresenter;

        private HeroController _heroController;
        private GameFieldController _fieldController;

        public override void OnCreate()
        {
            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };

            _hero = new HeroModel("Spidar Woman", 3, 5);

            _profilePresenter = new ProfilePresenter(_profile, Corner.TopRight);
            _heroPresenter = new HeroPresenter(_hero);
            _fieldPresenter = new GameFieldPresenter(_field);

            _heroController = new HeroController(_hero, _heroPresenter);
            _fieldController = new GameFieldController(_field, _fieldPresenter);
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            _fieldPresenter.Render(target);
            _profilePresenter.Render(target);
            _heroPresenter.Render(target);
        }

        public override void OnMouseMoved(int x, int y)
        {
            _fieldController.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (!_fieldPresenter.IsMouseWithinBounds(x, y)) return;
            
            _fieldController.OnMouseButtonPressed(x, y, mouseButton);
            _heroController.OnMouseButtonPressed(x, y, mouseButton);
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public override void OnWindowResized(int width, int height)
        {
            _profilePresenter.OnWindowResized(width, height);
        }
    }
}
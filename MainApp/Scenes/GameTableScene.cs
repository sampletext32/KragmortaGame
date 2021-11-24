using System.Collections.Generic;
using System.Globalization;
using MainApp.Controllers;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Handlers;
using MainApp.Layers;
using MainApp.Presenters;
using SFML.Graphics;

namespace MainApp.Scenes
{
    public class GameTableScene : Scene
    {
        #region Models

        private GameField _field;

        private List<HeroModel> _heroes;
        private List<MovementDeck> _decks;
        private Path _path;

        private Profile _profile;

        #endregion

        #region Presenters

        private ProfilePresenter _profilePresenter;
        private GameFieldPresenter _fieldPresenter;
        private MovementDeckPresenter _movementDeckPresenter;
        private List<HeroPresenter> _heroPresenters;
        private PathPresenter _pathPresenter;

        #endregion

        #region Controllers

        private GameFieldController _fieldController;
        private MovementDeckController _movementDeckController;
        private ShiftController _shiftController;
        private PathController _pathController;
        private List<HeroController> _heroControllers;

        #endregion

        #region Handlers

        private GameFieldHandler _gameFieldHandler;
        private MovementDeckHandler _movementDeckHandler;
        private PathHandler _pathHandler;
        private List<HeroHandler> _heroHandlers;

        #endregion

        private LayersStack _layersStack;

        public override void OnCreate()
        {
            InitAllModels();

            InitAllPresenters();

            InitAllControllers();

            InitAllHandlers();

            // Initiating LayersStack:
            _layersStack = new LayersStack(5);

            InitAllLayers();
        }

        private void InitAllModels()
        {
            // TODO: Remove hardcode with user's input of players number.
            var heroesCount = 2;

            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };
            _heroes = new List<HeroModel>(heroesCount);
            _heroes.Add(new HeroModel("Eggplant", 0, 0));
            _heroes.Add(new HeroModel("ABCDEF", 5, 0));

            _path = new Path();
        }

        private void InitAllPresenters()
        {
            _fieldPresenter = new GameFieldPresenter(_field);

            _pathPresenter = new PathPresenter(_path);

            _movementDeckPresenter = new MovementDeckPresenter();
            _movementDeckPresenter.SetDeck(_heroes[0].MovementDeck);

            _heroPresenters = new List<HeroPresenter>(2);
            _heroPresenters.Add(new HeroPresenter(_heroes[0]));
            _heroPresenters.Add(new HeroPresenter(_heroes[1]));
        }

        private void InitAllControllers()
        {
            _fieldController = new GameFieldController(_field);

            _heroControllers = new List<HeroController>(2);
            _heroControllers.Add(new HeroController(_heroes[0]));
            _heroControllers.Add(new HeroController(_heroes[1]));

            _shiftController = new ShiftController(_heroes, _heroControllers);

            _pathController = new PathController(_path);

            _movementDeckController = new MovementDeckController(_heroes[0].MovementDeck);
        }

        private void InitAllHandlers()
        {
            _gameFieldHandler = new GameFieldHandler(_fieldController, _movementDeckController, _pathController);
            _heroHandlers     = new List<HeroHandler>();
            _heroHandlers.Add(new HeroHandler(_heroControllers[0]));
            _heroHandlers.Add(new HeroHandler(_heroControllers[1]));
            _pathHandler = new PathHandler(_pathController, _fieldController, _movementDeckController, _shiftController);

            _movementDeckHandler = new MovementDeckHandler(_movementDeckController, _pathController, _shiftController, _fieldController);
        }

        private void InitAllLayers()
        {
            _layersStack.AddLayer(new GameFieldLayer(_fieldPresenter, _gameFieldHandler, "Game Field Layer"));
            _layersStack.AddLayer(new HeroLayer(_heroPresenters[0], _heroHandlers[0], $"\"{_heroes[0].Nickname}\" Hero Layer"));
            _layersStack.AddLayer(new HeroLayer(_heroPresenters[1], _heroHandlers[1], $"\"{_heroes[1].Nickname}\" Hero Layer"));
            _layersStack.AddLayer(new PathLayer(_pathPresenter, _pathHandler));
            _layersStack.AddLayer(new MovementDeckLayer(_movementDeckPresenter, _movementDeckHandler));
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            _layersStack.Render(target);
        }

        public override void OnMouseMoved(int x, int y)
        {
            _layersStack.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _layersStack.OnMousePressed(x, y, mouseButton);
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _layersStack.OnMouseReleased(x, y, mouseButton);
        }

        public override void OnWindowResized(int width, int height)
        {
            _layersStack.OnWindowResized(width, height);
        }
    }
}
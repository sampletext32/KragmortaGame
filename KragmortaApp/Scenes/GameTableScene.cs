using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;
using KragmortaApp.Handlers;
using KragmortaApp.Layers;
using KragmortaApp.Presenters;
using KragmortaApp.Enums;
using SFML.Graphics;

namespace KragmortaApp.Scenes
{
    public class GameTableScene : Scene
    {
        #region Models

        private int _heroCount = 2;

        private GameField _field;

        private List<HeroModel> _heroes;
        private List<MovementDeck> _decks;
        private Path _path;

        private Profile _profile;

        #endregion

        #region Presenters

        private ProfilePresenter _profilePresenter;
        private GameFieldPresenter _fieldPresenter;
        private MovementDecksPresenter _movementDecksPresenter;
        private List<HeroPresenter> _heroPresenters;
        private PathPresenter _pathPresenter;

        #endregion

        #region Controllers

        private GameFieldController _fieldController;
        private MovementDecksController _movementDecksController;
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

            // Initiating LayersStack (3 is gamefield + path + movement deck)
            _layersStack = new LayersStack(3 + _heroCount);

            InitAllLayers();
        }

        private void InitAllModels()
        {
            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };
            _heroes = new List<HeroModel>(_heroCount);

            for (int i = 0; i < _heroCount; i++)
            {
                _heroes.Add(new HeroModel($"Hero {i + 1}", i * 2, 0));
            }

            _path = new Path();
        }

        private void InitAllPresenters()
        {
            _fieldPresenter = new GameFieldPresenter(_field);

            _pathPresenter = new PathPresenter(_path);

            _movementDecksPresenter = new MovementDecksPresenter(_heroes.Select(h => h.MovementDeck).ToList());

            _heroPresenters = new List<HeroPresenter>(_heroCount);
            for (var i = 0; i < _heroCount; i++)
            {
                _heroPresenters.Add(new HeroPresenter(_heroes[i]));
            }
        }

        private void InitAllControllers()
        {
            _fieldController = new GameFieldController(_field);

            _heroControllers = new List<HeroController>(_heroCount);
            for (var i = 0; i < _heroCount; i++)
            {
                _heroControllers.Add(new HeroController(_heroes[i]));
            }

            _shiftController = new ShiftController(_heroes, _heroControllers);

            _pathController = new PathController(_path);

            _movementDecksController = new MovementDecksController(_heroes.Select(h => h.MovementDeck).ToList());
        }

        private void InitAllHandlers()
        {
            _gameFieldHandler = new GameFieldHandler(_fieldController, _movementDecksController, _pathController);
            _heroHandlers     = new List<HeroHandler>(_heroCount);
            for (var i = 0; i < _heroCount; i++)
            {
                _heroHandlers.Add(new HeroHandler(_heroControllers[i]));
            }

            _pathHandler = new PathHandler(_pathController, _fieldController, _movementDecksController, _shiftController);

            _movementDeckHandler = new MovementDeckHandler(_movementDecksController, _pathController, _shiftController, _fieldController);
        }

        private void InitAllLayers()
        {
            _layersStack.AddLayer(new GameFieldLayer(_fieldPresenter, _gameFieldHandler, "Game Field Layer"));
            
            for (var i = 0; i < _heroCount; i++)
            {
                _layersStack.AddLayer(new HeroLayer(_heroPresenters[i], _heroHandlers[i], $"\"{_heroes[i].Nickname}\" Hero Layer"));
            }
            _layersStack.AddLayer(new PathLayer(_pathPresenter, _pathHandler));
            _layersStack.AddLayer(new MovementDeckLayer(_movementDecksPresenter, _movementDeckHandler));
        }

        public override void OnUpdate(float deltaTime)
        {
            _layersStack.Update(deltaTime);
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
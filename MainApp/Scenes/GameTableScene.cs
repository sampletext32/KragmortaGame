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
        private List<PathCell> _path;

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

        #region Layers

        private LayersStack _layersStack;

        #endregion


        private Profile _profile;


        public override void OnCreate()
        {
            InitAllModels();

            InitAllPresenters();

            InitAllControllers();

            InitAllHandlers();

            // Initiating LayersStack:
            _layersStack = new LayersStack(4);

            InitAllLayers();

            #region OLD LOGIC

            // _profilePresenter = new ProfilePresenter(_profile, Corner.TopRight);
            // _fieldPresenter   = new GameFieldPresenter(_field);
            //
            // _movementDeckPresenter = new MovementDeckPresenter();
            //
            // _fieldController = new GameFieldController(_field, _fieldPresenter);
            // _shiftController = new ShiftController(2, _movementDeckPresenter, _fieldController);
            // _pathsController = new PathController(_field, _fieldPresenter, _shiftController);

            #endregion
        }

        private void InitAllModels()
        {
            // TODO: Remove hardcode with user's input of players number.
            var heroesCount = 1;

            _field = new GameField(10, 7);

            // Unused comment
            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };
            _heroes = new List<HeroModel>(heroesCount);
            _heroes.Add(new HeroModel("Nickname", 0, 0));

            _path = new List<PathCell>();
        }

        private void InitAllPresenters()
        {
            _fieldPresenter = new GameFieldPresenter(_field);

            _pathPresenter = new PathPresenter(_path);

            _movementDeckPresenter = new MovementDeckPresenter();
            _movementDeckPresenter.SetDeck(_heroes[0].MovementDeck);

            _heroPresenters = new List<HeroPresenter>(1);
            _heroPresenters.Add(new HeroPresenter(_heroes[0]));
        }

        private void InitAllControllers()
        {
            _fieldController = new GameFieldController(_field);

            _heroControllers = new List<HeroController>();
            _heroControllers.Add(new HeroController(_heroes[0]));

            _shiftController = new ShiftController(_heroes);

            _pathController = new PathController(_path);

            _movementDeckController = new MovementDeckController(_heroes[0].MovementDeck);
        }

        private void InitAllHandlers()
        {
            _gameFieldHandler = new GameFieldHandler(_fieldController, _movementDeckController, _pathController);
            _heroHandlers     = new List<HeroHandler>();
            _heroHandlers.Add(new HeroHandler(_heroControllers[0]));
            _pathHandler = new PathHandler(_pathController, _fieldController, _movementDeckController, _shiftController);

            _movementDeckHandler = new MovementDeckHandler(_movementDeckController, _pathController, _shiftController, _fieldController);
        }

        private void InitAllLayers()
        {
            _layersStack.AddLayer(new GameFieldLayer(_fieldPresenter, _gameFieldHandler, "Game Field Layer"));
            _layersStack.AddLayer(new HeroLayer(_heroPresenters[0], _heroHandlers[0]));
            _layersStack.AddLayer(new PathLayer(_pathPresenter, _pathHandler));
            _layersStack.AddLayer(new MovementDeckLayer(_movementDeckPresenter, _movementDeckHandler));
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            _layersStack.Render(target);

            #region OLD LOGIC

            // _fieldPresenter.Render(target);
            // _profilePresenter.Render(target);
            // foreach (var heroPresenter in _shiftController.HeroPresenters)
            // {
            //     heroPresenter.Render(target);
            // }
            //
            // _movementDeckPresenter.Render(target);

            #endregion
        }

        public override void OnMouseMoved(int x, int y)
        {
            _layersStack.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _layersStack.OnMousePressed(x, y, mouseButton);

            #region OLD LOGIC

            // if (_movementDeckPresenter.IsMouseWithinBounds(x, y))
            // {
            //     // _movementDeckController.OnMouseButtonPressed(x, y, mouseButton);
            //     _shiftController.MovementDeckController.OnMouseButtonPressed(x, y, mouseButton);
            //
            //     if (_shiftController.MovementDeckController.HasSelectedCard())
            //     {
            //         _pathsController.UnhighlightPaths();
            //         _pathsController.HighlightPaths();
            //     }
            //
            //     return;
            // }
            //
            // if (_fieldPresenter.IsMouseWithinBounds(x, y))
            // {
            //     _fieldController.OnMouseButtonPressed(x, y, mouseButton);
            //     _shiftController.OnMouseButtonPressed(x, y, mouseButton);
            //
            //     _pathsController.UnhighlightPaths();
            //     if (_shiftController.WasLastMoveSuccessful() &&
            //         _shiftController.MovementDeckController.HasActivatedCard())
            //     {
            //         _pathsController.HighlightPaths();
            //     }
            // }

            #endregion
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
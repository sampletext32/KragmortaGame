using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Controllers.ContextMenus;
using KragmortaApp.Entities.Buttons;
using KragmortaApp.Handlers;
using KragmortaApp.Layers;
using KragmortaApp.Presenters;
using KragmortaApp.Enums;
using KragmortaApp.Handlers.ContextMenus;
using KragmortaApp.Layers.ContextMenus;
using KragmortaApp.Presenters.ContextMenus;
using SFML.Graphics;
using SFML.Window;

namespace KragmortaApp.Scenes
{
    public class GameTableScene : Scene
    {
        #region Presenters

        private ProfilePresenter _profilePresenter;
        private GameFieldPresenter _fieldPresenter;
        private MovementDecksPresenter _movementDecksPresenter;
        private List<HeroPresenter> _heroPresenters;
        private PathPresenter _pathPresenter;
        private PushPresenter _pushPresenter;
        private PortalPresenter _portalPresenter;
        private MovementCardContextMenuPresenter _movementCardContextMenuPresenter;
        private FinishButtonPresenter _finishButtonPresenter;

        #endregion

        #region Controllers

        private GameFieldController _fieldController;
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;
        private PathController _pathController;
        private PushController _pushController;
        private PortalController _portalController;
        private List<HeroController> _heroControllers;
        private MovementCardContextMenuController _movementCardContextMenuController;
        private FinishButtonController _finishButtonController;

        #endregion

        #region Handlers

        private GameFieldHandler _gameFieldHandler;
        private MovementDeckHandler _movementDeckHandler;
        private PathHandler _pathHandler;
        private PushHandler _pushHandler;
        private PortalHandler _portalHandler;
        private List<HeroHandler> _heroHandlers;
        private MovementCardContextMenuHandler _movementCardContextMenuHandler;
        private FinishButtonHandler _finishButtonHandler;

        #endregion

        private LayersStack _layersStack;


        public override void OnCreate()
        {
            OnCreateCalled = true;

            InitAllPresenters();

            InitAllControllers();

            InitAllHandlers();


            InitAllLayers();
        }

        private void InitAllPresenters()
        {
            _fieldPresenter = new GameFieldPresenter(GameState.Instance.Field);

            _pathPresenter   = new PathPresenter(GameState.Instance.Path);
            _pushPresenter   = new PushPresenter(GameState.Instance.Push);
            _portalPresenter = new PortalPresenter(GameState.Instance.Portal);

            _movementDecksPresenter =
                new MovementDecksPresenter(GameState.Instance.Heroes.Select(h => h.MovementDeck).ToList(),
                    Corner.BottomRight);

            _heroPresenters = new List<HeroPresenter>(GameState.Instance.HeroCount);
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _heroPresenters.Add(new HeroPresenter(GameState.Instance.Heroes[i]));
            }

            _movementCardContextMenuPresenter =
                new MovementCardContextMenuPresenter(GameState.Instance.MovementCardContextMenuModel);

            _finishButtonPresenter = new FinishButtonPresenter(GameState.Instance.FinishButtonModel);
        }

        private void InitAllControllers()
        {
            _fieldController = new GameFieldController(GameState.Instance.Field);

            _heroControllers = new List<HeroController>(GameState.Instance.HeroCount);
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _heroControllers.Add(new HeroController(GameState.Instance.Heroes[i]));
            }

            _shiftController = new ShiftController(GameState.Instance.Heroes, _heroControllers);

            _pathController   = new PathController(GameState.Instance.Path);
            _pushController   = new PushController(GameState.Instance.Push);
            _portalController = new PortalController(GameState.Instance.Portal);

            _movementDecksController =
                new MovementDecksController(GameState.Instance.Heroes.Select(h => h.MovementDeck).ToList());
            _movementCardContextMenuController =
                new MovementCardContextMenuController(GameState.Instance.MovementCardContextMenuModel);

            //TODO: pass finishBtnModel
            _finishButtonController = new FinishButtonController(GameState.Instance.FinishButtonModel);
        }

        private void InitAllHandlers()
        {
            _gameFieldHandler = new GameFieldHandler(_fieldController, _movementDecksController, _pathController);
            _heroHandlers     = new List<HeroHandler>(GameState.Instance.HeroCount);
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _heroHandlers.Add(new HeroHandler(_heroControllers[i]));
            }

            _pathHandler = new PathHandler(_pathController, _pushController, _fieldController, _movementDecksController,
                _shiftController, _portalController);
            _pushHandler = new PushHandler(_pushController, _pathController, _fieldController, _movementDecksController,
                _shiftController);


            _portalHandler = new PortalHandler(_portalController, _shiftController, _movementDecksController,
                _fieldController, _pushController);


            _movementDeckHandler = new MovementDeckHandler(_movementDecksController, _pathController, _shiftController,
                _fieldController, _movementCardContextMenuController);

            _movementCardContextMenuHandler = new MovementCardContextMenuHandler(_movementCardContextMenuController,
                _movementDecksController, _shiftController, _pathController);
            _finishButtonHandler = new FinishButtonHandler(_movementDecksController, _shiftController, _pathController);
        }

        private void InitAllLayers()
        {
            // Initiating LayersStack (7 is gamefield + path + push + portal + movement deck + context menu + finish button)
            _layersStack = new LayersStack(7 + GameState.Instance.HeroCount);

            _layersStack.AddLayer(new GameFieldLayer(_fieldPresenter, _gameFieldHandler, "Game Field Layer"));
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _layersStack.AddLayer(new HeroLayer(_heroPresenters[i], _heroHandlers[i],
                    $"\"{GameState.Instance.Heroes[i].Nickname}\" Hero Layer"));
            }

            _layersStack.AddLayer(new PathLayer(_pathPresenter, _pathHandler));
            _layersStack.AddLayer(new PushLayer(_pushPresenter, _pushHandler));
            _layersStack.AddLayer(new PortalLayer(_portalPresenter, _portalHandler));
            _layersStack.AddLayer(new MovementDeckLayer(_movementDecksPresenter, _movementDeckHandler));
            _layersStack.AddLayer(new MovementCardContextMenuLayer(_movementCardContextMenuPresenter,
                _movementCardContextMenuHandler));
            _layersStack.AddLayer(new FinishButtonLayer(_finishButtonPresenter, _finishButtonHandler));
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

        public override void OnMouseScrolled(int x, int y, bool isVertical, float delta)
        {
            _layersStack?.OnMouseScrolled(x, y, isVertical, delta);
        }

        public override void OnKeyPressed(Keyboard.Key code)
        {
            if (code == Keyboard.Key.Escape)
            {
                Engine.Instance.PushScene(new PauseMenuScene());
            }
            
            _layersStack.OnKeyPressed(code);
        }
    }
}
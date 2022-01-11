using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Controllers.ContextMenus;
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

        private GameFieldPresenter _fieldPresenter;
        private MovementDecksPresenter _movementDecksPresenter;

        private List<HeroPresenter> _heroPresenters;
        private List<ProfilePresenter> _profilePresenters;
        private RigorPresenter _rigorPresenter;

        private PathPresenter _pathPresenter;
        private PushPresenter _pushPresenter;
        private PortalPresenter _portalPresenter;
        private MovementCardContextMenuPresenter _movementCardContextMenuPresenter;

        private WorkbenchPresenter _workbenchPresenter;

        private FinishButtonPresenter _finishButtonPresenter;
        // private BookshelfPresenter _bookshelfPresenter;

        #endregion

        #region Controllers

        private GameFieldController _fieldController;
        private MovementDecksController _movementDecksController;

        private ShiftController _shiftController;
        private List<HeroController> _heroControllers;
        private List<ProfileController> _profileControllers;

        private ProfilesController _profilesController;

        private RigorController _rigorController;

        private PathController _pathController;
        private PushController _pushController;
        private PortalController _portalController;
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
        private RigorHandler _rigorHandler;
        private MovementCardContextMenuHandler _movementCardContextMenuHandler;

        private FinishButtonHandler _finishButtonHandler;

        private WorkbenchHandler _workbenchHandler;
        private ProfilesHandler _profilesHandler;

        #endregion

        private LayersStack _layersStack;

        public bool InitStates = true;

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
            _portalPresenter = new PortalPresenter(GameState.Instance.Portals);

            _workbenchPresenter = new WorkbenchPresenter();

            _movementDecksPresenter =
                new MovementDecksPresenter(GameState.Instance.Heroes.Select(h => h.MovementDeck).ToList(),
                    Corner.BottomRight);

            _heroPresenters    = new List<HeroPresenter>(GameState.Instance.HeroCount);
            _profilePresenters = new List<ProfilePresenter>(GameState.Instance.HeroCount);
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _heroPresenters.Add(new HeroPresenter(GameState.Instance.Heroes[i], GameState.Instance.Profiles[i]));
                _profilePresenters.Add(new ProfilePresenter(GameState.Instance.Profiles[i]));
            }

            _rigorPresenter = new RigorPresenter(GameState.Instance.Rigor);

            _movementCardContextMenuPresenter =
                new MovementCardContextMenuPresenter(GameState.Instance.MovementCardContextMenuModel);

            _finishButtonPresenter = new FinishButtonPresenter(GameState.Instance.FinishButtonModel);
        }

        private void InitAllControllers()
        {
            _fieldController = new GameFieldController(GameState.Instance.Field, InitStates);

            _rigorController    = new RigorController(GameState.Instance.Rigor, InitStates);
            _heroControllers    = new List<HeroController>(GameState.Instance.HeroCount);
            _profileControllers = new List<ProfileController>(GameState.Instance.HeroCount);
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _heroControllers.Add(new HeroController(GameState.Instance.Heroes[i], InitStates));
                _profileControllers.Add(new ProfileController(GameState.Instance.Profiles[i], InitStates));
            }

            _profilesController = new ProfilesController(GameState.Instance.Profiles, _profileControllers, InitStates);

            _shiftController = new ShiftController(GameState.Instance.Heroes, _heroControllers, InitStates);

            _pathController   = new PathController(GameState.Instance.Path, InitStates);
            _pushController   = new PushController(GameState.Instance.Push, GameState.Instance.PushedStateModel, InitStates);
            _portalController = new PortalController(GameState.Instance.Portals, InitStates);

            _movementDecksController =
                new MovementDecksController(GameState.Instance.Heroes.Select(h => h.MovementDeck).ToList(), InitStates);
            _movementCardContextMenuController =
                new MovementCardContextMenuController(GameState.Instance.MovementCardContextMenuModel, InitStates);

            _finishButtonController = new FinishButtonController(GameState.Instance.FinishButtonModel, InitStates);
        }

        private void InitAllHandlers()
        {
            _gameFieldHandler = new GameFieldHandler(_fieldController, _movementDecksController, _pathController);
            _rigorHandler     = new RigorHandler(_rigorController);
            _heroHandlers     = new List<HeroHandler>(GameState.Instance.HeroCount);
            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _heroHandlers.Add(new HeroHandler(_heroControllers[i]));
            }

            _pathHandler = new PathHandler(_pathController, _pushController, _fieldController, _movementDecksController,
                _shiftController, _portalController, _finishButtonController, _profilesController, _rigorController);
            _pushHandler = new PushHandler(_pushController, _pathController, _fieldController, _movementDecksController,
                _shiftController, _finishButtonController, _portalController, _profilesController, _rigorController);


            _portalHandler = new PortalHandler(_portalController, _shiftController, _movementDecksController,
                _fieldController, _pushController, _finishButtonController, _profilesController, _rigorController);

            _workbenchHandler = new WorkbenchHandler();

            _movementDeckHandler = new MovementDeckHandler(_movementDecksController, _pathController, _shiftController,
                _fieldController, _movementCardContextMenuController, _rigorController);

            _movementCardContextMenuHandler = new MovementCardContextMenuHandler(_movementCardContextMenuController,
                _movementDecksController, _shiftController, _pathController);
            _finishButtonHandler = new FinishButtonHandler(_movementDecksController, _shiftController, _pathController, _profilesController);

            _profilesHandler = new ProfilesHandler();
        }

        private void InitAllLayers()
        {
            InitLayersStack();


            _layersStack.AddLayer(new GameFieldLayer(_fieldPresenter, _gameFieldHandler, "Game Field Layer"));

            _layersStack.AddLayer(new RigorLayer(_rigorPresenter, _rigorHandler));

            for (var i = 0; i < GameState.Instance.HeroCount; i++)
            {
                _layersStack.AddLayer(new HeroLayer(_heroPresenters[i], _heroHandlers[i],
                    $"\"{GameState.Instance.Profiles[i].Nickname}\" Hero Layer"));
                _layersStack.AddLayer(new ProfileLayer(_profilePresenters[i], _profilesHandler));
            }

            _layersStack.AddLayer(new PathLayer(_pathPresenter, _pathHandler));

            _layersStack.AddLayer(new PushLayer(_pushPresenter, _pushHandler));

            _layersStack.AddLayer(new PortalLayer(_portalPresenter, _portalHandler));

            _layersStack.AddLayer(new WorkbenchLayer(_workbenchPresenter, _workbenchHandler));
            
            _layersStack.AddLayer(new MovementDeckLayer(_movementDecksPresenter, _movementDeckHandler));

            _layersStack.AddLayer(new MovementCardContextMenuLayer(_movementCardContextMenuPresenter,
                _movementCardContextMenuHandler));

            _layersStack.AddLayer(new FinishButtonLayer(_finishButtonPresenter, _finishButtonHandler));
        }

        /// <summary>
        /// Initiating LayersStack
        /// </summary>
        /// <remarks>
        /// 11 is gamefield + rigor + path + push + portal + workbench + movement deck + context menu + finish button + profiles + bookshelf
        /// </remarks>
        /// <param name="layersNumber">Number of the initiating layers.</param>
        private void InitLayersStack(int layersNumber = 11)
        {
            _layersStack = new LayersStack(layersNumber + GameState.Instance.HeroCount * 2);
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
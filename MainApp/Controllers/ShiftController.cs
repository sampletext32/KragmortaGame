using System.Collections.Generic;
using MainApp.Entities;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class ShiftController
    {
        public IReadOnlyList<HeroPresenter> HeroPresenters => _heroPresenters;
        public MovementDeckController MovementDeckController => _movementDeckControllers[_currentHeroIndex];
        public HeroModel HeroModel => _heroModels[_currentHeroIndex];
        public HeroController HeroController => _heroControllers[_currentHeroIndex];
        public HeroPresenter HeroPresenter => _heroPresenters[_currentHeroIndex];
        
        private List<HeroModel> _heroModels;
        private List<HeroController> _heroControllers;
        private List<HeroPresenter> _heroPresenters;
        private int _currentHeroIndex = 0;

        private readonly int _countOfPlayers;
        private MovementDeckPresenter _movementDeckPresenter;

        private GameFieldController _gameFieldController;

        private List<MovementDeckController> _movementDeckControllers;

        private int _currentHeroSuccessfulMovesCount = 0;

        public ShiftController(
            int countOfPlayers,
            MovementDeckPresenter movementDeckPresenter,
            GameFieldController gameFieldController
        )
        {
            _countOfPlayers        = countOfPlayers;
            _heroModels            = new List<HeroModel>(countOfPlayers);
            _movementDeckPresenter = movementDeckPresenter;
            _heroPresenters        = new List<HeroPresenter>(countOfPlayers);

            _gameFieldController     = gameFieldController;
            _heroControllers         = new List<HeroController>(countOfPlayers);
            _movementDeckControllers = new List<MovementDeckController>(countOfPlayers);

            for (var i = 0; i < countOfPlayers; i++)
            {
                var hero = new HeroModel($"Pl{i + 1}", i * 2, 0);
                _heroModels.Add(hero);

                var heroPresenter = new HeroPresenter(hero);
                _heroPresenters.Add(heroPresenter);

                var movementDeckController = new MovementDeckController(hero.MovementDeck, _movementDeckPresenter);
                _movementDeckControllers.Add(movementDeckController);

                _heroControllers.Add(new HeroController(
                    hero,
                    heroPresenter,
                    movementDeckController,
                    _gameFieldController));
            }

            _heroControllers[0].Activate();
            _movementDeckPresenter.SetDeck(_heroModels[_currentHeroIndex].MovementDeck);
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            HeroController.OnMouseButtonPressed(x, y, mouseButton);
            if (HeroController.WasLastMoveSuccessful)
            {
                _currentHeroSuccessfulMovesCount++;
                if (_currentHeroSuccessfulMovesCount == 2)
                {
                    HeroController.Deactivate();
                    _currentHeroIndex           = (_currentHeroIndex + 1) % _countOfPlayers;
                    HeroController.Activate();
                    _movementDeckPresenter.SetDeck(HeroModel.MovementDeck);
                    _currentHeroSuccessfulMovesCount = 0;
                }
            }
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            HeroController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public bool WasLastMoveSuccessful()
        {
            return HeroController.WasLastMoveSuccessful;
        }
    }
}
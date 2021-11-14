using System.Collections.Generic;
using MainApp.Entities.Models;
using MainApp.Entities.Presenters;

namespace MainApp.Entities.Controllers
{
    public class ShiftController
    {
        public IReadOnlyList<HeroPresenter> HeroPresenters => _heroPresenters;
        public MovementDeckController MovementDeckController => _movementDeckControllers[_currentHeroIndex];

        private List<HeroModel> _heroModels;
        private List<HeroController> _heroControllers;
        private List<HeroPresenter> _heroPresenters;
        private int _currentHeroIndex = 0;
        
        private MovementDeckPresenter _movementDeckPresenter;
        
        private GameFieldController _gameFieldController;
        
        private List<MovementDeckController> _movementDeckControllers;

        public ShiftController(int countOfPlayers, MovementDeckPresenter movementDeckPresenter,
            GameFieldController gameFieldController)
        {
            _heroModels            = new List<HeroModel>(countOfPlayers);
            _movementDeckPresenter = movementDeckPresenter;
            _heroPresenters        = new List<HeroPresenter>(countOfPlayers);

            _gameFieldController     = gameFieldController;
            _heroControllers         = new List<HeroController>(countOfPlayers);
            _movementDeckControllers = new List<MovementDeckController>(countOfPlayers);

            for (var i = 0; i < countOfPlayers; i++)
            {
                var hero          = new HeroModel($"Pl{i + 1}", i, i);
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
            
            _movementDeckPresenter.SetDeck(_heroModels[_currentHeroIndex].MovementDeck);
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _heroControllers[_currentHeroIndex].OnMouseButtonPressed(x, y, mouseButton);
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _heroControllers[_currentHeroIndex].OnMouseButtonReleased(x, y, mouseButton);
        }
    }
}
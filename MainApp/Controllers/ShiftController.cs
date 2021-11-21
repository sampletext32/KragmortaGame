using System.Collections.Generic;
using MainApp.Entities;

namespace MainApp.Controllers
{
    public class ShiftController : ControllerBase
    {
        public HeroModel Hero => _heroModels[_currentHeroIndex];
        
        private List<HeroModel> _heroModels;
        private List<HeroController> _heroControllers;
        private int _currentHeroIndex = 0;

        private readonly int _countOfPlayers;


        private int _currentHeroSuccessfulMovesCount = 0;

        public ShiftController(List<HeroModel> heroes)
        {
            _heroModels      = heroes;
            _heroControllers = new List<HeroController>(heroes.Count);

            for (var i = 0; i < heroes.Count; i++)
            {
                _heroControllers.Add(
                    new HeroController(
                        heroes[i]
                        // _gameFieldController
                    ));
            }

            _heroControllers[0].Activate();
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            // HeroController.OnMouseButtonPressed(x, y, mouseButton);
            // if (HeroController.WasLastMoveSuccessful)
            // {
            //     _currentHeroSuccessfulMovesCount++;
            //     if (_currentHeroSuccessfulMovesCount == 2)
            //     {
            //         HeroController.Deactivate();
            //         _currentHeroIndex = (_currentHeroIndex + 1) % _countOfPlayers;
            //         HeroController.Activate();
            //         _movementDeckPresenter.SetDeck(HeroModel.MovementDeck);
            //         _currentHeroSuccessfulMovesCount = 0;
            //     }
            // }
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            // HeroController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public override void OnMouseMoved(int x, int y)
        {
        }

        public bool WasLastMoveSuccessful()
        {
            return false;
            // return HeroController.WasLastMoveSuccessful;
        }
    }
}
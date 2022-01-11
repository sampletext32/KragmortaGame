using System.Collections.Generic;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class ShiftController : ControllerBase
    {
        /// <summary>
        /// Current hero.
        /// </summary>
        public HeroModel Hero => _heroModels[_currentHeroIndex];

        public HeroController HeroController => _heroControllers[_currentHeroIndex];
        public IHero WhoEnteredPortal;

        private IReadOnlyList<HeroModel> _heroModels;
        private List<HeroController> _heroControllers;
        private int _currentHeroIndex = 0;

        public ShiftController(IReadOnlyList<HeroModel> heroes, List<HeroController> controllers, bool initStates)
        {
            _heroModels      = heroes;
            _heroControllers = controllers;

            if (initStates)
            {
                _heroControllers[0].Activate();
            }
            else
            {
                _currentHeroIndex = GameState.Instance.CurrentPlayerIndex;
            }
        }

        public void ActivateNextPlayer()
        {
            HeroController.Deactivate();
            _currentHeroIndex                     = (_currentHeroIndex + 1) % _heroModels.Count;
            GameState.Instance.CurrentPlayerIndex = _currentHeroIndex;
            HeroController.Activate();
        }

        public void NoticeCardDeletion()
        {
            HeroController.NoticeCardDeletion();
        }

        public bool HasAnyCardDeletionsLeft()
        {
            return HeroController.HasAnyCardDeletionsLeft();
        }
    }
}
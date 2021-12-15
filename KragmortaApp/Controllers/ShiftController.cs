using System.Collections.Generic;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class ShiftController : ControllerBase
    {
        public HeroModel Hero => _heroModels[_currentHeroIndex];
        public HeroController HeroController => _heroControllers[_currentHeroIndex];

        private IReadOnlyList<HeroModel> _heroModels;
        private List<HeroController> _heroControllers;
        private int _currentHeroIndex = 0;

        public ShiftController(IReadOnlyList<HeroModel> heroes, List<HeroController> controllers)
        {
            _heroModels      = heroes;
            _heroControllers = controllers;
            _heroControllers[0].Activate();
        }

        public void ActivateNextPlayer()
        {
            HeroController.Deactivate();
            _currentHeroIndex  = (_currentHeroIndex + 1) % _heroModels.Count;
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
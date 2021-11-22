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
                _heroControllers.Add(new HeroController(heroes[i]));
            }

            _heroControllers[0].Activate();
        }
    }
}
using System.Collections.Generic;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class ProfilesController
    {
        public ProfileController CurrentController => _controllers[_currentHeroIndex];
        private List<ProfileController> _controllers;

        public Profile CurrentProfile => _profiles[_currentHeroIndex];
        private IReadOnlyList<Profile> _profiles;
        private int _currentHeroIndex = 0;

        private int _booksNumber = 0;

        public ProfilesController(IReadOnlyList<Profile> profiles, List<ProfileController> controllers, bool initStates)
        {
            _profiles              = profiles;
            _controllers           = controllers;

            if (initStates)
            {
                CurrentController.Activate();
            }
            else
            {
                _currentHeroIndex = GameState.Instance.CurrentPlayerIndex;
            }
        }
        
        public void ActivateNextPlayer()
        {
            CurrentController.Deactivate();
            _currentHeroIndex = (_currentHeroIndex + 1) % _profiles.Count;
            CurrentController.Activate();
        }

        public void DealDamageToHero(HeroModel victimHero)
        {
            _controllers[(int)victimHero.Id - 1].TakeDamage();
        }

        public void GiveBookToActive()
        {
            ++_booksNumber;
            CurrentController.GiveBook();
            
            if (_booksNumber == GameState.Instance.BookLimit)
            {
                // TODO: Finish game.
            }
        }

        public void GiveBookToHero(HeroModel victim)
        {
            ++_booksNumber;
            _controllers[(int)victim.Id - 1].GiveBook();
            
            if (_booksNumber == GameState.Instance.BookLimit)
            {
                // TODO: Finish game.
            }
        }
    }
}
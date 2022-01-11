﻿using System.Collections.Generic;
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

        public ProfilesController(IReadOnlyList<Profile> profiles, List<ProfileController> controllers)
        {
            _profiles              = profiles;
            _controllers           = controllers;
            CurrentController.Activate();
        }
        
        public void ActivateNextPlayer()
        {
            CurrentController.Deactivate();
            _currentHeroIndex = (_currentHeroIndex + 1) % _profiles.Count;
            CurrentController.Activate();
        }
    }
}
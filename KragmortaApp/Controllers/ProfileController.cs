using System;
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class ProfileController
    {
        private Profile _profile;

        private Random _random;

        public ProfileController(Profile profile, bool initStates)
        {
            _profile = profile;
            _random  = new Random(DateTime.Now.Millisecond);
        }

        public void Activate()
        {
            _profile.Activated   = true;
            _profile.MarkDirty();
        }

        public void Deactivate()
        {
            _profile.Activated   = false;
            _profile.MarkDirty();
        }

        public void TakeDamage()
        {
            _profile.Lives--;
            _profile.MarkDirty();
        }

        public void GiveBook()
        {
            _profile.MagicBooks.Add(new MagicBook(_random.Next(1, 6)));
            _profile.MarkDirty();
        }
    }
}
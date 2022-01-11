using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class ProfileController
    {
        private Profile _profile;

        public ProfileController(Profile profile, bool initStates)
        {
            _profile = profile;
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
    }
}
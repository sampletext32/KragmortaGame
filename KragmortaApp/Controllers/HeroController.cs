using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class HeroController : ControllerBase
    {
        private HeroModel _hero;

        public HeroController(HeroModel hero, bool initStates)
        {
            _hero = hero;
        }

        public void Activate()
        {
            _hero.Activated = true;
            _hero.CardDeletionLeft   = 1;
            _hero.MarkDirty();
        }

        public void Deactivate()
        {
            _hero.Activated = false;
            _hero.MarkDirty();
        }

        public void NoticeCardDeletion()
        {
            _hero.CardDeletionLeft--;
        }

        public bool HasAnyCardDeletionsLeft()
        {
            return _hero.CardDeletionLeft > 0;
        }
    }
}
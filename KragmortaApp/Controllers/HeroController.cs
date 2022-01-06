using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class HeroController : ControllerBase
    {
        private HeroModel _hero;
        private int _cardDeletionLeft = 1;

        public HeroController(HeroModel hero)
        {
            _hero = hero;
        }

        public void Activate()
        {
            _hero.Activated = true;
            _cardDeletionLeft   = 1;
            _hero.MarkDirty();
        }

        public void Deactivate()
        {
            _hero.Activated = false;
            _hero.MarkDirty();
        }

        public void NoticeCardDeletion()
        {
            _cardDeletionLeft--;
        }

        public bool HasAnyCardDeletionsLeft()
        {
            return _cardDeletionLeft > 0;
        }
    }
}
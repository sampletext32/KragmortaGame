using MainApp.Entities.Models;
using MainApp.Entities.Presenters;

namespace MainApp.Entities.Controllers
{
    public class HeroController
    {
        private HeroModel _hero;
        private HeroPresenter _heroPresenter;

        public HeroController(HeroModel hero, HeroPresenter heroPresenter)
        {
            _hero = hero;
            _heroPresenter = heroPresenter;
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;
            
            
            var cX = _heroPresenter.ConvertMouseXToCellX(x);
            var cY = _heroPresenter.ConvertMouseYToCellY(y);

            _hero.SetFieldPosition(cX, cY);
            _heroPresenter.OnHeroMoved();
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
        }
    }
}
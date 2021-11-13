namespace MainApp.Entities
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
            if (_heroPresenter.IsMouseWithinBounds(x, y))
            {
                return;
            }

            if (mouseButton == KragMouseButton.Right)
            {
                return;
            }

            var cX = _heroPresenter.ConvertMouseXToCellX(x);
            var cY = _heroPresenter.ConvertMouseYToCellY(y);

            _hero.SetFieldPosition(cX, cY);
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            
        }
    }
}
using System;
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
            
            
            var selectedCellX = _heroPresenter.ConvertMouseXToCellX(x);
            var selectedCellY = _heroPresenter.ConvertMouseYToCellY(y);

            if (IsSelectedCellNeighboring(selectedCellX, selectedCellY))
            {
                _hero.SetFieldPosition(selectedCellX, selectedCellY);
                _heroPresenter.OnHeroMoved();
            }
            else
            {
                Console.WriteLine("Hero can move only on a neighboring cell: either vertically or horizontally");
            }
        }

        private bool IsSelectedCellNeighboring(int selectedCellX, int selectedCellY)
        {
            var xAxis = Math.Abs(_hero.FieldX - selectedCellX);
            var yAxis = Math.Abs(_hero.FieldY - selectedCellY);

            return xAxis + yAxis == 1;
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
        }
    }
}
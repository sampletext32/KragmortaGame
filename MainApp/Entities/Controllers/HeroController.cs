using System;
using MainApp.Entities.Models;
using MainApp.Entities.Presenters;

namespace MainApp.Entities.Controllers
{
    public class HeroController
    {
        private HeroModel _hero;
        private HeroPresenter _heroPresenter;
        private MovementDeckController _movementDeckController;
        private GameFieldController _gameFieldController;

        public HeroController(HeroModel hero, HeroPresenter heroPresenter, MovementDeckController movementDeckController, GameFieldController gameFieldController)
        {
            _hero                     = hero;
            _heroPresenter            = heroPresenter;
            _movementDeckController   = movementDeckController;
            _gameFieldController = gameFieldController;
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            if (!_movementDeckController.HasSelectedCard() && !_movementDeckController.HasActivatedCard())
            {
                return;
            }

            var selectedCellX = _heroPresenter.ConvertMouseXToCellX(x);
            var selectedCellY = _heroPresenter.ConvertMouseYToCellY(y);

            if (IsSelectedCellNeighboring(selectedCellX, selectedCellY))
            {
                var fieldType = _gameFieldController.GetFieldType(selectedCellX, selectedCellY);
                if (_movementDeckController.TryUseCellType(fieldType))
                {
                    _hero.SetFieldPosition(selectedCellX, selectedCellY);
                    _heroPresenter.OnHeroMoved();
                }
                else
                {
                    Console.WriteLine("Unable to move the hero");
                }
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
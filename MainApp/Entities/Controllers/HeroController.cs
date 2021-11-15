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

        public bool WasLastMoveSuccessful { get; private set; }

        public HeroController(HeroModel hero, HeroPresenter heroPresenter, MovementDeckController movementDeckController, GameFieldController gameFieldController)
        {
            _hero                   = hero;
            _heroPresenter          = heroPresenter;
            _movementDeckController = movementDeckController;
            _gameFieldController    = gameFieldController;
        }

        public void Activate()
        {
            _hero.IsCurrentHero = true;
            _heroPresenter.OnHeroActivated();
        }

        public void Deactivate()
        {
            _hero.IsCurrentHero = false;
            _heroPresenter.OnHeroDeactivated();
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            WasLastMoveSuccessful = false;

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
                    WasLastMoveSuccessful = true;
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
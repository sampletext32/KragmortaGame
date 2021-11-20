using System;
using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class PathHandler : Handler
    {
        private GameFieldController _gameFieldController;
        private MovementDeckController _movementDeckController;
        private ShiftController _shiftController;

        public PathHandler(PathController controller, GameFieldController gameFieldController,
            MovementDeckController movementDeckController, ShiftController shiftController) : base(controller)
        {
            _gameFieldController    = gameFieldController;
            _movementDeckController = movementDeckController;
            _shiftController        = shiftController;
        }

        public override void OnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            if (!_movementDeckController.HasSelectedCard() && !_movementDeckController.HasActivatedCard())
            {
                return;
            }
            
            if (IsSelectedCellNeighboring(selectedCellX, selectedCellY))
            {
                var fieldType = _gameFieldController.GetCellType(selectedCellX, selectedCellY);
                if (_movementDeckController.TryUseCellType(fieldType))
                {
                    _shiftController.Hero.SetFieldPosition(selectedCellX, selectedCellY);
                    _heroPresenter.OnHeroMoved(); // TODO: Add "dirty" flags to all models, so that only "dirty" models are refreshed.
                    WasLastMoveSuccessful = true;
                }
            }
            else
            {
                Console.WriteLine("Hero can move only on a neighboring cell: either vertically or horizontally");
            }
            
            
            if (HeroController.WasLastMoveSuccessful)
            {
                _currentHeroSuccessfulMovesCount++;
                if (_currentHeroSuccessfulMovesCount == 2)
                {
                    HeroController.Deactivate();
                    _currentHeroIndex = (_currentHeroIndex + 1) % _countOfPlayers;
                    HeroController.Activate();
                    _movementDeckPresenter.SetDeck(HeroModel.MovementDeck);
                    _currentHeroSuccessfulMovesCount = 0;
                }
            }
        }
        private bool IsSelectedCellNeighboring(int selectedCellX, int selectedCellY)
        {
            var xAxis = Math.Abs(_shiftController.Hero.FieldX - selectedCellX);
            var yAxis = Math.Abs(_shiftController.Hero.FieldY - selectedCellY);

            return xAxis + yAxis == 1;
        }

        public override void OnMouseMoved(int x, int y)
        {
        }

        public override void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void OnMouseLeft()
        {
        }
    }
}
using System;
using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class PathHandler : AbstractHandler
    {
        private readonly PathController _pathController;
        private GameFieldController _gameFieldController;
        private MovementDeckController _movementDeckController;
        private ShiftController _shiftController;

        public PathHandler(PathController pathController, GameFieldController gameFieldController,
            MovementDeckController movementDeckController, ShiftController shiftController) : base(pathController)
        {
            _pathController         = pathController;
            _gameFieldController    = gameFieldController;
            _movementDeckController = movementDeckController;
            _shiftController        = shiftController;
        }

        public override void RawOnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
        }

        public void OnCellClicked(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
            if (!_pathController.TryGetCell(selectedCellX, selectedCellY, out var pathCell))
            {
                return;
            }

            if (mouseButton != KragMouseButton.Left) return;

            _movementDeckController.UseCellType(pathCell.Type);
            _shiftController.Hero.SetFieldPosition(selectedCellX, selectedCellY);
        }

        public override void RawOnMouseMoved(int x, int y)
        {
        }

        public override void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void RawOnMouseLeft()
        {
        }
    }
}
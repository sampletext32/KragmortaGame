using KragmortaApp.Controllers;

namespace KragmortaApp.Handlers
{
    public class FinishButtonHandler : AbstractHandler
    {
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;
        private PathController _pathController;

        public FinishButtonHandler(MovementDecksController movementDecksController, ShiftController shiftController,
            PathController pathController)
        {
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
            _pathController          = pathController;
        }

        public void FinishTurn()
        {
            _shiftController.ActivateNextPlayer();

            if (_movementDecksController.HasActivatedCard())
            {
                _pathController.ClearPaths();

                _movementDecksController.DismissActivatedCard();

                _movementDecksController.PullNewCard();
            }
            else if (_movementDecksController.HasSelectedCard())
            {
                _pathController.ClearPaths();
            }

            _movementDecksController.ActivateNextDeck();
        }
    }
}
using KragmortaApp.Controllers;

namespace KragmortaApp.Handlers
{
    public class FinishButtonHandler : AbstractHandler
    {
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;
        private PathController _pathController;
        private ProfilesController _profilesController;

        public FinishButtonHandler(
            MovementDecksController movementDecksController,
            ShiftController shiftController,
            PathController pathController,
            ProfilesController profilesController
        )
        {
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
            _pathController          = pathController;
            _profilesController      = profilesController;
        }

        public void FinishTurn()
        {
            _shiftController.ActivateNextPlayer();
            _profilesController.ActivateNextPlayer();

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
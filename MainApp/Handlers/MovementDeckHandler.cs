using MainApp.Controllers;
using MainApp.Entities;

namespace MainApp.Handlers
{
    public class MovementDeckHandler : AbstractHandler
    {
        private PathController _pathsController;
        private ShiftController _shiftController;
        private MovementDeckController _movementDeckController;

        public MovementDeckHandler(MovementDeckController controller, ShiftController shiftController, PathController pathsController) : base(controller)
        {
            _shiftController = shiftController;
            _pathsController = pathsController;
        }

        public void OnCardPressed(MovementCard card)
        {
            _pathsController.UnhighlightPaths();
            if (_shiftController.WasLastMoveSuccessful() &&
                _movementDeckController.HasActivatedCard())
            {
                _pathsController.HighlightPaths();
            }
        }
    }
}
using MainApp.Controllers;
using MainApp.Entities;

namespace MainApp.Handlers
{
    public class MovementDeckHandler : AbstractHandler
    {
        private GameFieldController _fieldController;
        private PathController _pathsController;
        private ShiftController _shiftController;
        private MovementDeckController _movementDeckController;

        public MovementDeckHandler(MovementDeckController controller, GameFieldController fieldController, ShiftController shiftController, PathController pathsController) : base(controller)
        {
            _fieldController = fieldController;
            _shiftController = shiftController;
            _pathsController = pathsController;
        }

        public void OnMousePressedCard(MovementCard card)
        {
            _pathsController.UnhighlightPaths();
            if (_shiftController.WasLastMoveSuccessful() &&
                _movementDeckController.HasActivatedCard())
            {
                _pathsController.HighlightPaths();
            }
        }

        public override void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _movementDeckController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public override void RawOnMouseLeft()
        {
        }
    }
}
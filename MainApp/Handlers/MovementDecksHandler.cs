using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class MovementDecksHandler : Handler
    {
        private GameFieldController _fieldController;
        private PathController _pathsController;
        private ShiftController _shiftController;
        private MovementDeckController _movementDeckController;

        public MovementDecksHandler(MovementDeckController controller, GameFieldController fieldController, ShiftController shiftController, PathController pathsController) : base(controller)
        {
            _fieldController = fieldController;
            _shiftController = shiftController;
            _pathsController = pathsController;
        }

        public override void OnMouseMoved(int x, int y)
        {
        }

        public override void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.OnMouseButtonPressed(x, y, mouseButton);
            _shiftController.OnMouseButtonPressed(x, y, mouseButton);

            _pathsController.UnhighlightPaths();
            if (_shiftController.WasLastMoveSuccessful() &&
                _movementDeckController.HasActivatedCard())
            {
                _pathsController.HighlightPaths();
            }
        }

        public override void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _movementDeckController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public override void OnMouseLeft()
        {
        }
    }
}
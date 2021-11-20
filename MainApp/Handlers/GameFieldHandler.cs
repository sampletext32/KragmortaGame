using MainApp.Controllers;
using MainApp.Entities;

namespace MainApp.Handlers
{
    public class GameFieldHandler : Handler
    {
        private GameFieldController _fieldController;
        private MovementDeckController _movementDeckController;
        private PathController _pathController;

        public GameFieldHandler(GameFieldController controller, GameFieldController fieldController,
            MovementDeckController movementDeckController, PathController pathController) : base(controller)
        {
            _fieldController        = fieldController;
            _movementDeckController = movementDeckController;
            _pathController         = pathController;
        }

        public override void OnMouseMoved(int x, int y)
        {
            _fieldController.OnMouseMoved(x, y);
        }

        public void OnMouseLeft(int x, int y)
        {
            _fieldController.OnMouseExit();
        }

        public override void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            _movementDeckController.OnMouseButtonPressed(x, y, mouseButton);

            if (_movementDeckController.HasSelectedCard())
            {
                _pathController.UnhighlightPaths();
                _pathController.HighlightPaths();
            }
        }

        public override void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.OnMouseButtonReleased(x, y, mouseButton);
        }
    }
}
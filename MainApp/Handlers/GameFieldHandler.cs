using MainApp.Controllers;
using MainApp.Entities;

namespace MainApp.Handlers
{
    public class GameFieldHandler : Handler
    {
        private GameFieldController _fieldController;
        private MovementDeckController _movementDeckController;
        private PathController _pathController;

        public GameFieldHandler(
            GameFieldController controller,
            MovementDeckController movementDeckController,
            PathController pathController
        ) : base(controller)
        {
            _fieldController        = controller;
            _movementDeckController = movementDeckController;
            _pathController         = pathController;
        }

        public override void OnMouseMoved(int x, int y)
        {
            _fieldController.OnMouseMoved(x, y);
        }

        public override void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.OnMouseButtonPressed(x, y, mouseButton);
            // _movementDeckController.OnMouseButtonPressed(x, y, mouseButton);
            //
            // if (_movementDeckController.HasSelectedCard())
            // {
            //     _pathController.UnhighlightPaths();
            //     _pathController.HighlightPaths();
            // }
        }

        public override void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public override void OnMouseLeft()
        {
            _fieldController.OnMouseLeft();
        }
    }
}
using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class GameFieldHandler : AbstractHandler
    {
        private GameFieldController _gameFieldController;
        private MovementDeckController _movementDeckController;
        private PathController _pathController;

        public GameFieldHandler(
            GameFieldController controller,
            MovementDeckController movementDeckController,
            PathController pathController
        )
        {
            _gameFieldController    = controller;
            _movementDeckController = movementDeckController;
            _pathController         = pathController;
        }

        public void OnMouseMovedOverCell(int cellX, int cellY)
        {
            var fieldCell = _gameFieldController.GetCell(cellX, cellY);
            _gameFieldController.HoverCell(fieldCell);
        }

        public void OnMousePressedCell(int cellX, int cellY)
        {
            var fieldCell = _gameFieldController.GetCell(cellX, cellY);
            _gameFieldController.PressCell(fieldCell);
        }

        public override void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _gameFieldController.ReleaseLastPressedCell();
        }

        public void OnMouseLeft()
        {
            _gameFieldController.ClearLastHoveredCell();
        }
    }
}
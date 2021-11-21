using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class GameFieldHandler : AbstractHandler
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

        public void OnMouseMovedOverCell(int cellX, int cellY)
        {
            var fieldCell = _fieldController.GetCell(cellX, cellY);
            _fieldController.HoverCell(fieldCell);
        }

        public void OnMousePressedCell(int cellX, int cellY)
        {
            var fieldCell = _fieldController.GetCell(cellX, cellY);
            _fieldController.PressCell(fieldCell);
        }

        public override void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.ReleaseLastPressedCell();
        }

        public void OnMouseLeft()
        {
            _fieldController.ClearLastHoveredCell();
        }
    }
}
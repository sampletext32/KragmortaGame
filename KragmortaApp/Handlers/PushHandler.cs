using System;
using KragmortaApp.Controllers;

namespace KragmortaApp.Handlers
{
    public class PushHandler : AbstractHandler
    {
        private readonly PushController _pushController;
        private GameFieldController _gameFieldController;
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;

        public PushHandler(
            PushController pushController,
            GameFieldController gameFieldController,
            MovementDecksController movementDecksController,
            ShiftController shiftController
        )
        {
            _pushController          = pushController;
            _gameFieldController     = gameFieldController;
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
        }

        public override void RawOnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
        }
        
        public void OnPushCellClicked(int pathCellX, int pathCellY, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            Console.WriteLine("Resolving Push");
            // TODO
        }
    }
}
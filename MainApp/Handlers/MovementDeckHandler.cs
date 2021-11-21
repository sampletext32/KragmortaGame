using System.Collections.Generic;
using MainApp.Controllers;
using MainApp.Entities;

namespace MainApp.Handlers
{
    public class MovementDeckHandler : AbstractHandler
    {
        private PathController _pathController;
        private ShiftController _shiftController;
        private MovementDeckController _movementDeckController;
        private GameFieldController _fieldController;

        public MovementDeckHandler(MovementDeckController movementDeckController,
            PathController pathController, ShiftController shiftController, GameFieldController fieldController) : base(movementDeckController)
        {
            _shiftController        = shiftController;
            _fieldController   = fieldController;
            _pathController         = pathController;
            _movementDeckController = movementDeckController;
        }

        public void OnCardPressed(MovementCard card)
        {
            if (card is null) return;
            
            // If a card has already been selected, nothing happens.
            if (_movementDeckController.HasActivatedCard()) return;

            // if (_movementDeckController.LastSelectedMovementCard == card) return;

            _movementDeckController.SelectedCard(card);
            
            var heroX = _shiftController.Hero.FieldX;
            var heroY = _shiftController.Hero.FieldY;

            List<AbstractCell> rawPaths = _fieldController.GetNeighboringCells(heroX, heroY);
            
            _pathController.ComputePath(rawPaths, card);



            // _pathController.UnhighlightPaths();
            // if (_shiftController.WasLastMoveSuccessful() &&
            //     _movementDeckController.HasActivatedCard())
            // {
            //     _pathController.HighlightPaths();
            // }
        }
    }
}
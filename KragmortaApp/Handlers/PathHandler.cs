using System;
using System.Collections.Generic;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;

namespace KragmortaApp.Handlers
{
    public class PathHandler : AbstractHandler
    {
        private readonly PathController _pathController;
        private GameFieldController _gameFieldController;
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;

        // TODO: encapsulate this list inside path controller (duplicate with MovementDeckHandler)
        private List<AbstractCell> _rawPaths;

        public PathHandler(
            PathController pathController,
            GameFieldController gameFieldController,
            MovementDecksController movementDecksController,
            ShiftController shiftController
        )
        {
            _pathController          = pathController;
            _gameFieldController     = gameFieldController;
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
            _rawPaths                = new List<AbstractCell>(4);
        }

        public override void RawOnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
        }

        public void OnPathCellClicked(int pathCellX, int pathCellY, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            // NOTE: no need to check the result of "Try", because a selected cell here is known to be the path cell
            _pathController.TryGetCell(pathCellX, pathCellY, out var pathCell);

            // Here we have 3 phases. 
            // 1 - no card is selected
            // 2 - a card is selected
            // 3 - a card is activated

            if (_movementDecksController.HasSelectedCard())
            {
                // case 2
                _movementDecksController.ActivateSelectedCard();
                
                _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);
                
                // regenerate visible path
                _rawPaths.Clear();
                _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _rawPaths);

                _pathController.SetVisiblePath(_rawPaths, _movementDecksController.ActivatedMovementCard);
            }
            else if (_movementDecksController.HasActivatedCard())
            {
                // case 3 
                _movementDecksController.SpendType(pathCell.Type);
                _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);

                _movementDecksController.DismissActivatedCard();


                // clear visible path
                _rawPaths.Clear();
                _pathController.SetVisiblePath(_rawPaths, null);

                _shiftController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();
            }
            else
            {
                //case 1
                throw new KragException("Unreachable");
            }
        }

        public override void RawOnMouseMoved(int x, int y)
        {
        }

        public override void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void RawOnMouseLeft()
        {
        }
    }
}
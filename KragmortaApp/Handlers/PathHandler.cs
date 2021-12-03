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
            _pathController         = pathController;
            _gameFieldController    = gameFieldController;
            _movementDecksController = movementDecksController;
            _shiftController        = shiftController;
            _rawPaths               = new List<AbstractCell>(4);
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

            if (!_movementDecksController.HasSelectedCard())
            {
                // case 1
                // not possible to reach in this method, because no path cells are present
                throw new KragException("Unreachable");
            }
            else
            {
                // case 2
                _movementDecksController.ActivateSelectedCard();
            }

            // case 3

            _movementDecksController.SpendType(pathCell.Type);

            _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);

            if (_movementDecksController.ActivatedMovementCard.HasUsedFirstType &&
                _movementDecksController.ActivatedMovementCard.HasUsedSecondType)
            {
                _movementDecksController.DismissActivatedCard();
            }

            // by now, we could dismiss the card, if it was our second move, so we need to check for card availability
            if (_movementDecksController.HasActivatedCard())
            {
                // regenerate visible path
                _rawPaths.Clear();
                _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _rawPaths);

                _pathController.SetVisiblePath(_rawPaths, _movementDecksController.ActivatedMovementCard);
            }
            else
            {
                // clear visible path
                _rawPaths.Clear();
                _pathController.SetVisiblePath(_rawPaths, null);

                _shiftController.ActivateNextPlayer();
                // _movementDeckController.SetActiveDeck(_shiftController.Hero.MovementDeck);
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
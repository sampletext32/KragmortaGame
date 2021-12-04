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

                _movementDecksController.SpendType(pathCell.Type);
                _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);

                // regenerate visible path
                _pathController.RawPath.Clear();
                _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _pathController.RawPath);

                if (!_pathController.TrySetVisiblePath(_movementDecksController.ActivatedMovementCard))
                {
                    Console.WriteLine("No moves with selected card");
                    _movementDecksController.DismissActivatedCard();
                    _movementDecksController.PullNewCard();
                    _shiftController.ActivateNextPlayer();
                    _movementDecksController.ActivateNextDeck();
                }
            }
            else if (_movementDecksController.HasActivatedCard())
            {
                // case 3 
                _movementDecksController.SpendType(pathCell.Type);
                _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);

                _movementDecksController.DismissActivatedCard();
                _movementDecksController.PullNewCard();

                // clear visible path
                _pathController.RawPath.Clear();
                _pathController.TrySetVisiblePath(null);

                _shiftController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();
            }
            else
            {
                //case 1
                throw new KragException("Unreachable");
            }
        }
    }
}
using System;
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

        // TODO: encapsulate this list inside path controller (duplicate with PathHandler)
        private List<AbstractCell> _rawPaths;

        public MovementDeckHandler(MovementDeckController movementDeckController,
            PathController pathController, ShiftController shiftController, GameFieldController fieldController) : base(movementDeckController)
        {
            _shiftController        = shiftController;
            _fieldController        = fieldController;
            _pathController         = pathController;
            _movementDeckController = movementDeckController;
            _rawPaths               = new List<AbstractCell>(4);
        }

        public void OnCardPressed(MovementCard card)
        {
            // Here we have 3 phases. (reversed order of checking)
            // 1 - a card is activated
            // 2 - a card is selected
            // 3 - no card is selected

            // case 1
            if (_movementDeckController.HasActivatedCard())
            {
                Console.WriteLine("Can't select a card, because there is already an activated one");
                return;
            }

            // case 2
            if (_movementDeckController.HasSelectedCard())
            {
                if (_movementDeckController.LastSelectedMovementCard != card)
                {
                    _movementDeckController.UnselectCard();
                    _movementDeckController.SelectCard(card);

                    var heroX = _shiftController.Hero.FieldX;
                    var heroY = _shiftController.Hero.FieldY;

                    _rawPaths.Clear();
                    _fieldController.CollectNeighboringCells(heroX, heroY, _rawPaths);

                    _pathController.SetVisiblePath(_rawPaths, card);
                }
                else
                {
                    _movementDeckController.UnselectCard();
                    // NOTE: we can pass a null card, because for empty path cells it won't be accessed
                    _rawPaths.Clear();
                    _pathController.SetVisiblePath(_rawPaths, null);
                }

                return;
            }

            // case 3
            if (!_movementDeckController.HasSelectedCard())
            {
                _movementDeckController.SelectCard(card);

                var heroX = _shiftController.Hero.FieldX;
                var heroY = _shiftController.Hero.FieldY;

                _rawPaths.Clear();
                _fieldController.CollectNeighboringCells(heroX, heroY, _rawPaths);

                _pathController.SetVisiblePath(_rawPaths, card);
                return;
            }
        }
    }
}
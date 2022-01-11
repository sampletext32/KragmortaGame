using System;
using KragmortaApp.Controllers;
using KragmortaApp.Controllers.ContextMenus;
using KragmortaApp.Entities;

namespace KragmortaApp.Handlers
{
    public class MovementDeckHandler : AbstractHandler
    {
        private PathController _pathController;
        private ShiftController _shiftController;
        private MovementDecksController _movementDecksController;
        private GameFieldController _fieldController;
        private MovementCardContextMenuController _movementCardContextMenuController;
        private RigorController _rigorController;

        public MovementDeckHandler(
            MovementDecksController movementDecksController,
            PathController pathController,
            ShiftController shiftController,
            GameFieldController fieldController,
            MovementCardContextMenuController movementCardContextMenuController,
            RigorController rigorController)
        {
            _shiftController                   = shiftController;
            _fieldController                   = fieldController;
            _movementCardContextMenuController = movementCardContextMenuController;
            _rigorController                   = rigorController;
            _pathController                    = pathController;
            _movementDecksController           = movementDecksController;
        }

        public void OnCardPressed(MovementCard card)
        {
            // Here we have 3 phases. (reversed order of checking)
            // 1 - a card is activated
            // 2 - a card is selected
            // 3 - no card is selected

            // case 1
            if (_movementDecksController.HasActivatedCard())
            {
                Console.WriteLine("Can't select a card, because there is already an activated one");
                return;
            }

            // case 2
            if (_movementDecksController.HasSelectedCard())
            {
                if (_movementDecksController.LastSelectedMovementCard != card)
                {
                    _movementDecksController.UnselectCard();
                    _movementDecksController.SelectCard(card);


                    int heroX, heroY;
                    if (_movementDecksController.LastSelectedMovementCard.MovementCardType == MovementCardType.Goblin)
                    {
                        heroX = _shiftController.Hero.FieldX;
                        heroY = _shiftController.Hero.FieldY;
                    }
                    else
                    {
                        heroX = _rigorController.Model.FieldX;
                        heroY = _rigorController.Model.FieldY;
                    }

                    _pathController.RawPath.Clear();
                    _fieldController.CollectNeighboringCells(heroX, heroY, _pathController.RawPath);

                    _pathController.TrySetVisiblePath(card);
                }
                else
                {
                    _movementDecksController.UnselectCard();
                    // NOTE: we can pass a null card, because for empty path cells it won't be accessed
                    _pathController.RawPath.Clear();
                    _pathController.TrySetVisiblePath(null);
                }

                return;
            }

            // case 3
            {
                _movementDecksController.SelectCard(card);

                int heroX, heroY;
                if (_movementDecksController.LastSelectedMovementCard.MovementCardType == MovementCardType.Goblin)
                {
                    heroX = _shiftController.Hero.FieldX;
                    heroY = _shiftController.Hero.FieldY;
                }
                else
                {
                    heroX = _rigorController.Model.FieldX;
                    heroY = _rigorController.Model.FieldY;
                }

                _pathController.RawPath.Clear();
                _fieldController.CollectNeighboringCells(heroX, heroY, _pathController.RawPath);

                _pathController.TrySetVisiblePath(card);
            }
        }

        public void ContextMenuFor(MovementCard card, int x, int y)
        {
            if (!_shiftController.HasAnyCardDeletionsLeft()) return;

            if (_movementDecksController.HasActivatedCard()) return;

            _movementCardContextMenuController.SetPosition(x, y);
            _movementCardContextMenuController.SetCard(card);
        }
    }
}
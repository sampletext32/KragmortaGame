﻿using System;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class MovementDeckController
    {
        public event Action<MovementCard> CardSelected;
        public event Action<MovementCard> CardActivated;

        private MovementDeck _deck;
        private MovementDeckPresenter _presenter;

        private int _lastSelectedMovementCardIndex = -1;
        private MovementCard _lastSelectedMovementCard = null;
        private MovementCard _activatedMovementCard = null;
        private int _activatedMovementCardIndex;
        private HeroController _heroController;

        public MovementDeckController(MovementDeck deck, MovementDeckPresenter presenter)
        {
            _deck      = deck;
            _presenter = presenter;
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (_activatedMovementCard != null)
            {
                return;
            }

            var cardIndex = _presenter.GetCardIndex(x, y);
            if (cardIndex == -1) return;

            var movementCard = _deck.MovementCards[cardIndex];

            if (_lastSelectedMovementCard != null && _lastSelectedMovementCard != movementCard)
            {
                _lastSelectedMovementCard.Selected = false;
                _presenter.UpdateCardAtPosition(_lastSelectedMovementCardIndex);
                _lastSelectedMovementCard      = null;
                _lastSelectedMovementCardIndex = -1;
            }

            movementCard.Selected = !movementCard.Selected;

            _presenter.UpdateCardAtPosition(cardIndex);
            _lastSelectedMovementCard      = movementCard;
            _lastSelectedMovementCardIndex = cardIndex;
            
            CardSelected?.Invoke(movementCard);
        }

        public bool HasSelectedCard()
        {
            return _lastSelectedMovementCard is not null;
        }

        private void ActivateSelectedCard()
        {
            _lastSelectedMovementCard.Activated = true;
            _lastSelectedMovementCard.Selected  = false;

            _activatedMovementCard      = _lastSelectedMovementCard;
            _activatedMovementCardIndex = _lastSelectedMovementCardIndex;

            _presenter.UpdateCardAtPosition(_lastSelectedMovementCardIndex);

            _lastSelectedMovementCard      = null;
            _lastSelectedMovementCardIndex = -1;
            
            CardActivated?.Invoke(_activatedMovementCard);
        }

        public bool HasActivatedCard()
        {
            return _activatedMovementCard is not null;
        }

        public MovementCard GetSelectedCard()
        {
            return _lastSelectedMovementCard;
        }

        public MovementCard GetActivatedCard()
        {
            return _activatedMovementCard;
        }

        public bool TryUseCellType(CellType cellType)
        {
            if (HasSelectedCard() &&
                (_lastSelectedMovementCard.FirstType == cellType || _lastSelectedMovementCard.SecondType == cellType))
            {
                ActivateSelectedCard();
            }

            if (HasActivatedCard())
            {
                if (_activatedMovementCard.FirstType == cellType && !_activatedMovementCard.HasUsedFirstType)
                {
                    _activatedMovementCard.HasUsedFirstType = true;
                }
                else if (_activatedMovementCard.SecondType == cellType && !_activatedMovementCard.HasUsedSecondType)
                {
                    _activatedMovementCard.HasUsedSecondType = true;
                }
                else
                {
                    return false;
                }

                if (_activatedMovementCard.HasUsedFirstType && _activatedMovementCard.HasUsedSecondType)
                {
                    _activatedMovementCard.Activated = false;

                    _deck.DismissCard(_activatedMovementCard);
                    _presenter.RemoveCardAtPosition(_activatedMovementCardIndex);
                    _activatedMovementCard      = null;
                    _activatedMovementCardIndex = -1;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
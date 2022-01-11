using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Enums;

namespace KragmortaApp.Controllers
{
    /// <summary>
    /// Controls movement decks
    /// </summary>
    public class MovementDecksController : ControllerBase
    {
        public MovementCard LastSelectedMovementCard => _lastSelectedMovementCard;
        public MovementCard ActivatedMovementCard => _activatedMovementCard;

        public MovementDeck CurrentDeck => _decks[_currentDeckIndex];

        private List<MovementDeck> _decks;

        private int _currentDeckIndex = 0;

        private MovementCard _lastSelectedMovementCard = null;

        private MovementCard _activatedMovementCard = null;

        public MovementDecksController(List<MovementDeck> decks, bool initStates)
        {
            _decks              = decks;

            if (initStates)
            {
                CurrentDeck.Visible = true;
                CurrentDeck.MarkDirty();
            }
        }

        public bool HasSelectedCard()
        {
            return _lastSelectedMovementCard is not null;
        }

        public bool HasActivatedCard()
        {
            return _activatedMovementCard is not null;
        }

        public void ActivateSelectedCard()
        {
            _lastSelectedMovementCard.Activated = true;
            _lastSelectedMovementCard.Selected  = false;

            _activatedMovementCard = _lastSelectedMovementCard;

            _activatedMovementCard.MarkDirty();

            _lastSelectedMovementCard = null;
        }

        public void SpendType(CellType cellType)
        {
            if (!_activatedMovementCard.HasUsedFirstType &&
                (_activatedMovementCard.FirstType & cellType) != CellType.Empty)
            {
                _activatedMovementCard.HasUsedFirstType = true;
            }
            else if (!_activatedMovementCard.HasUsedSecondType &&
                     (_activatedMovementCard.SecondType & cellType) != CellType.Empty)
            {
                _activatedMovementCard.HasUsedSecondType = true;
            }
        }

        public void UnselectCard()
        {
            _lastSelectedMovementCard.Selected = false;
            _lastSelectedMovementCard.MarkDirty();
            _lastSelectedMovementCard = null;
        }

        public void SelectCard(MovementCard card)
        {
            card.Selected = true;
            card.MarkDirty();
            _lastSelectedMovementCard = card;
        }

        public void DismissActivatedCard()
        {
            CurrentDeck.RemoveCard(_activatedMovementCard);

            _activatedMovementCard = null;

            CurrentDeck.MarkDirty();
        }

        public void ActivateNextDeck()
        {
            CurrentDeck.Visible = false;
            CurrentDeck.MarkDirty();

            if (HasSelectedCard())
            {
                _lastSelectedMovementCard.Selected = false;
                _lastSelectedMovementCard.MarkDirty();
                _lastSelectedMovementCard = null;
            }

            if (HasActivatedCard())
            {
                _activatedMovementCard.Activated = false;
                _activatedMovementCard.MarkDirty();
                _activatedMovementCard = null;
            }

            _currentDeckIndex = (_currentDeckIndex + 1) % _decks.Count;

            CurrentDeck.Visible = true;
            CurrentDeck.MarkDirty();
        }

        public void PullNewCard()
        {
            CurrentDeck.AddCard(MovementCard.Generate());
        }

        public void DismissCard(MovementCard movementCard)
        {
            CurrentDeck.RemoveCard(movementCard);

            CurrentDeck.MarkDirty();
        }
    }
}
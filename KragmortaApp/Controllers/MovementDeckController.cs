using KragmortaApp.Entities;
using KragmortaApp.Enums;

namespace KragmortaApp.Controllers
{
    /// <summary>
    /// Controls a single movement deck
    /// </summary>
    public class MovementDeckController : ControllerBase
    {
        public MovementCard LastSelectedMovementCard => _lastSelectedMovementCard;
        public MovementCard ActivatedMovementCard => _activatedMovementCard;

        private MovementDeck _deck;

        private MovementCard _lastSelectedMovementCard = null;

        private MovementCard _activatedMovementCard = null;

        public MovementDeckController(MovementDeck deck)
        {
            _deck = deck;
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

            _activatedMovementCard      = _lastSelectedMovementCard;

            _activatedMovementCard.MarkDirty();

            _lastSelectedMovementCard      = null;
        }

        public void SpendType(CellType cellType)
        {
            if (!_activatedMovementCard.HasUsedFirstType && (_activatedMovementCard.FirstType & cellType) != CellType.Empty)
            {
                _activatedMovementCard.HasUsedFirstType = true;
            }
            else if (!_activatedMovementCard.HasUsedSecondType && (_activatedMovementCard.SecondType & cellType) != CellType.Empty)
            {
                _activatedMovementCard.HasUsedSecondType = true;
            }
        }

        public void UnselectCard()
        {
            _lastSelectedMovementCard.Selected = false;
            _lastSelectedMovementCard.MarkDirty();
            _lastSelectedMovementCard          = null;
        }

        public void SelectCard(MovementCard card)
        {
            card.Selected             = true;
            card.MarkDirty();
            _lastSelectedMovementCard = card;
        }

        public void DismissActivatedCard()
        {
            _deck.RemoveCard(_activatedMovementCard);

            _activatedMovementCard      = null;

            _deck.MarkDirty();
        }
    }
}
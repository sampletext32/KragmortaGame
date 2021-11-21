using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class MovementDeckController : ControllerBase
    {
        private MovementDeck _deck;

        private int _lastSelectedMovementCardIndex = -1;
        private MovementCard _lastSelectedMovementCard = null;
        private MovementCard _activatedMovementCard = null;
        private int _activatedMovementCardIndex;

        public MovementDeckController(MovementDeck deck)
        {
            _deck = deck;
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            // If a card has already been selected, nothing happens. 
            // if (_activatedMovementCard != null)
            // {
            //     return;
            // }
            //
            // var cardIndex = _presenter.GetCardIndex(x, y);
            // if (cardIndex == -1) return;
            //
            // var movementCard = _deck.MovementCards[cardIndex];
            //
            // // If we have selected a card before and already selected card is not the one we have just selected,  
            // if (_lastSelectedMovementCard != null && _lastSelectedMovementCard != movementCard)
            // {
            //     // we unselect the previous card and
            //     _lastSelectedMovementCard.Selected = false;
            //     _presenter.UpdateCardAtPosition(_lastSelectedMovementCardIndex);
            //     _lastSelectedMovementCard      = null;
            //     _lastSelectedMovementCardIndex = -1;
            // }
            //
            // // change the selected card flag to true
            // movementCard.Selected = !movementCard.Selected;
            //
            // _presenter.UpdateCardAtPosition(cardIndex);
            // _lastSelectedMovementCard      = movementCard;
            // _lastSelectedMovementCardIndex = cardIndex;
            //
            // // Card has been successfully changed.
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void OnMouseMoved(int x, int y)
        {
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

            _activatedMovementCard.MarkDirty();

            _lastSelectedMovementCard      = null;
            _lastSelectedMovementCardIndex = -1;
        }

        public bool HasActivatedCard()
        {
            return _activatedMovementCard is not null;
        }

        public void UseCellType(CellType cellType)
        {
            if (HasSelectedCard())
            {
                ActivateSelectedCard();
            }

            if (_activatedMovementCard.FirstType == cellType && !_activatedMovementCard.HasUsedFirstType)
            {
                _activatedMovementCard.HasUsedFirstType = true;
            }
            else if (_activatedMovementCard.SecondType == cellType && !_activatedMovementCard.HasUsedSecondType)
            {
                _activatedMovementCard.HasUsedSecondType = true;
            }

            // TODO
            // if (_activatedMovementCard.HasUsedFirstType && _activatedMovementCard.HasUsedSecondType)
            // {
            //     _activatedMovementCard.Activated = false;
            //
            //     _deck.DismissCard(_activatedMovementCard);
            //     _presenter.RemoveCardAtPosition(_activatedMovementCardIndex);
            //     _activatedMovementCard      = null;
            //     _activatedMovementCardIndex = -1;
            // }
        }
    }
}
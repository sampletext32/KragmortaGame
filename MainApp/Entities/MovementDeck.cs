#nullable enable
using System.Collections.Generic;

namespace MainApp.Entities
{
    public class MovementDeck
    {
        public IReadOnlyList<MovementCard> MovementCards => _movementCards;

        public MovementCard? GetUsingMovementCard()
        {
            foreach (var movementCard in _movementCards)
            {
                if (movementCard.Selected || movementCard.Activated)
                {
                    return movementCard;
                }
            }

            return null;
        }

        private readonly List<MovementCard> _movementCards;

        public MovementDeck()
        {
            _movementCards = new List<MovementCard>();
        }

        public void AddCard(MovementCard card)
        {
            _movementCards.Add(card);
        }

        public void DismissCard(MovementCard card)
        {
            _movementCards.Remove(card);
        }
    }
}
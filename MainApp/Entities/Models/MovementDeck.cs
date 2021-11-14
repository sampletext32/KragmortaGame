using System.Collections.Generic;

namespace MainApp.Entities.Models
{
    public class MovementDeck
    {
        public IReadOnlyList<MovementCard> MovementCards => _movementCards;

        private readonly List<MovementCard> _movementCards;

        public MovementDeck()
        {
            _movementCards = new List<MovementCard>();
        }

        public void AddCard(MovementCard card)
        {
            _movementCards.Add(card);
        }
    }
}
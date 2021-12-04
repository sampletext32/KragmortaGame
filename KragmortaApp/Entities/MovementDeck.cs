using System.Collections.Generic;

namespace KragmortaApp.Entities
{
    public class MovementDeck : VisualEntity
    {
        public IReadOnlyList<MovementCard> MovementCards => _movementCards;

        private readonly List<MovementCard> _movementCards;
        
        public bool Visible { get; set; }

        public MovementDeck()
        {
            _movementCards = new List<MovementCard>();
        }

        public void AddCard(MovementCard card)
        {
            _movementCards.Add(card);
            card.MarkDirty();
        }

        public void RemoveCard(MovementCard card)
        {
            _movementCards.Remove(card);
            card.MarkDirty();
        }
    }
}
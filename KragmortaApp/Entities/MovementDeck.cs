using System.Collections.Generic;
using System.Linq;
using KragmortaApp.FileDatas;

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

        public MovementDeck(MovementDeckFileData fileData)
        {
            _movementCards = fileData.MovementCards.Select(f => new MovementCard(f)).ToList();
            Visible        = fileData.Visible;
        }

        public MovementDeckFileData ToFileData()
        {
            return new MovementDeckFileData()
            {
                Visible       = Visible,
                MovementCards = MovementCards.Select(c => c.ToFileData()).ToList()
            };
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
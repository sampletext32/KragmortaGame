using MainApp.Enums;

namespace MainApp.Entities
{
    // 56 cards in a deck 

    public class MovementCard : VisualEntity
    {
        public CellType FirstType => _firstType;
        public CellType SecondType => _secondType;

        private CellType _firstType;
        private CellType _secondType;
        
        public bool Selected { get; set; }

        public bool HasUsedFirstType { get; set; }
        public bool HasUsedSecondType { get; set; }
        public bool Activated { get; set; }

        public MovementCard(CellType firstType, CellType secondType)
        {
            _firstType  = firstType;
            _secondType = secondType;
        }

        /// <summary>
        /// Clones the current card, without any used moves
        /// </summary>
        public MovementCard Clone()
        {
            var movementCard = new MovementCard(_firstType, _secondType);

            return movementCard;
        }
    }
}
using System.Collections.Generic;
using MainApp.Entities.Enums;

namespace MainApp.Entities.Models
{
    // 56 cards in a deck 

    public class MovementCard
    {
        public FieldType FirstType => _firstType;
        public FieldType SecondType => _secondType;
        
        private FieldType _firstType;
        private FieldType _secondType;
        
        public bool HasUsedFirstType { get; set; }
        public bool HasUsedSecondType { get; set; }
        
        public MovementCard(FieldType firstType,FieldType secondType)
        {
            _firstType = firstType;
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
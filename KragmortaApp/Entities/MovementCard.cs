using System;
using KragmortaApp.Enums;
using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
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

        private static Random _random = new Random(DateTime.Now.Millisecond);

        public MovementCard(MovementCardFileData fileData)
        {
            _firstType        = fileData.FirstType;
            _secondType       = fileData.SecondType;
            Selected          = fileData.Selected;
            HasUsedFirstType  = fileData.HasUsedFirstType;
            HasUsedSecondType = fileData.HasUsedSecondType;
            Activated         = fileData.Activated;
        }

        public MovementCardFileData ToFileData()
        {
            return new MovementCardFileData()
            {
                FirstType         = FirstType,
                SecondType        = SecondType,
                Selected          = Selected,
                HasUsedFirstType  = HasUsedFirstType,
                HasUsedSecondType = HasUsedSecondType,
                Activated         = Activated,
            };
        }
        
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

        public static MovementCard Generate()
        {
            int first = _random.Next(0, 4);
            int second = _random.Next(0, 4);

            return new MovementCard((CellType)(1 << first), (CellType)(1 << second));
        }
    }
}
using System;
using KragmortaApp.Enums;
using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
{
    // 56 cards in a deck 

    public enum MovementCardType
    {
        Goblin,
        Rigor
    }
    public class MovementCard : VisualEntity
    {
        public CellType FirstType => _firstType;
        public CellType SecondType => _secondType;
        public MovementCardType MovementCardType => _movementCardType;

        private CellType _firstType;
        private CellType _secondType;
        private readonly MovementCardType _movementCardType;

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
        
        public MovementCard(CellType firstType, CellType secondType, MovementCardType movementCardType)
        {
            _firstType             = firstType;
            _secondType            = secondType;
            _movementCardType = movementCardType;
        }

        public static MovementCard Generate()
        {
            int first = _random.Next(0, 5);
            int second = _random.Next(0, 5);

            if (_random.Next(0, 100) < 30)
            {
                return new RigorMovementCard((CellType)(1 << first), (CellType)(1 << second));
            }

            return new GoblinMovementCard((CellType)(1 << first), (CellType)(1 << second));

            // return new MovementCard((CellType)(1 << first), (CellType)(1 << second));
        }
    }
}
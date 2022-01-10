using KragmortaApp.Enums;

namespace KragmortaApp.Entities
{
    public class RigorMovementCard : MovementCard
    {
        public RigorMovementCard(CellType firstType, CellType secondType) : base(firstType, secondType, MovementCardType.Rigor)
        {
        }
    }
}
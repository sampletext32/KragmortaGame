using KragmortaApp.Enums;

namespace KragmortaApp.Entities
{
    public class GoblinMovementCard : MovementCard
    {
        public GoblinMovementCard(CellType firstType, CellType secondType) : base(firstType, secondType, MovementCardType.Goblin)
        {
        }
    }
}
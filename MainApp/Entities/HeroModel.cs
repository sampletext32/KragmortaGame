using MainApp.Enums;

namespace MainApp.Entities
{
    public class HeroModel : VisualEntity
    {
        public string Nickname { get; private set; }

        public int FieldX { get; private set; }

        public int FieldY { get; private set; }

        public bool IsCurrentHero { get; set; }

        public readonly MovementDeck MovementDeck;

        public HeroModel(string nickname, int fieldX, int fieldY)
        {
            Nickname = nickname;
            FieldX   = fieldX;
            FieldY   = fieldY;

            MovementDeck = new MovementDeck();

            MovementDeck.AddCard(new MovementCard(CellType.Red, CellType.Green));
            MovementDeck.AddCard(new MovementCard(CellType.Green, CellType.Orange));
            MovementDeck.AddCard(new MovementCard(CellType.Blue, CellType.Red));
        }

        public void SetFieldPosition(int x, int y)
        {
            FieldX = x;
            FieldY = y;
            Dirty  = true;
        }
    }
}
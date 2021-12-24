namespace KragmortaApp.Entities
{
    public class HeroModel : VisualEntity
    {
        public long Id { get; }
        public string Nickname { get; private set; }

        public int FieldX { get; private set; }

        public int FieldY { get; private set; }

        public bool IsCurrentHero { get; set; }

        public readonly MovementDeck MovementDeck;

        public HeroModel(long id, string nickname, int fieldX, int fieldY)
        {
            Nickname = nickname;
            FieldX   = fieldX;
            FieldY   = fieldY;
            Id  = id;

            MovementDeck = new MovementDeck();

            MovementDeck.AddCard(MovementCard.Generate());
            MovementDeck.AddCard(MovementCard.Generate());
            MovementDeck.AddCard(MovementCard.Generate());
        }

        public void SetFieldPosition(int x, int y)
        {
            FieldX = x;
            FieldY = y;
            Dirty  = true;
        }
    }
}
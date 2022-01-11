using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
{
    public class HeroModel : VisualEntity
    {
        public long Id { get; private set; }

        public int FieldX { get; private set; }

        public int FieldY { get; private set; }

        public bool Activated { get; set; }

        public readonly MovementDeck MovementDeck;

        public HeroModel(HeroFileData fileData)
        {
            Id     = fileData.Id;
            FieldX = fileData.FieldX;
            FieldY = fileData.FieldY;

            MovementDeck = new MovementDeck(fileData.MovementDeck);
        }

        public HeroFileData ToFileData()
        {
            return new HeroFileData()
            {
                Id = Id,
                FieldX = FieldX,
                FieldY = FieldY,
                Activated    = Activated,
                MovementDeck = MovementDeck.ToFileData()
            };
        }

        public HeroModel(long id, int fieldX, int fieldY)
        {
            Id     = id;
            FieldX = fieldX;
            FieldY = fieldY;

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
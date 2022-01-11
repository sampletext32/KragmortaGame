namespace KragmortaApp.FileDatas
{
    public class HeroFileData
    {
        public long Id { get; set; }

        public int FieldX { get; set; }

        public int FieldY { get; set; }

        public bool Activated { get; set; }

        public MovementDeckFileData MovementDeck { get; set; }
    }
}
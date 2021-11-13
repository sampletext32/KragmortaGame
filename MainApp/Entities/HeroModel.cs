namespace MainApp.Entities
{
    public class HeroModel
    {
        public string Nickname => _nickname;
        public int FieldX;
        public int FieldY;

        private string _nickname;

        public HeroModel(string nickname, int fieldX, int fieldY)
        {
            _nickname = nickname;
            FieldX = fieldX;
            FieldY = fieldY;
        }
    }
}
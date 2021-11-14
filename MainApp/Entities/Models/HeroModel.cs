using System;

namespace MainApp.Entities.Models
{
    public class HeroModel
    {
        public string Nickname => _nickname;
        public int FieldX
        {
            get => _fieldX;
            set
            {
                _fieldX = value;
            }
        }

        public int FieldY
        {
            get => _fieldY;
            set
            {
                _fieldY = value;
            }
        }

        public void SetFieldPosition(int x, int y)
        {
            _fieldX = x;
            _fieldY = y;
        }
        
        private int _fieldX;
        private int _fieldY;

        private string _nickname;

        public HeroModel(string nickname, int fieldX, int fieldY)
        {
            _nickname = nickname;
            _fieldX = fieldX;
            _fieldY = fieldY;
        }
    }
}
using System;
using System.Collections.Generic;

namespace MainApp.Entities
{
    public class HeroModel
    {
        public event Action LocationChanged; 
        public string Nickname => _nickname;
        public int FieldX
        {
            get => _fieldX;
            set
            {
                _fieldX = value;
                LocationChanged?.Invoke();
            }
        }

        public int FieldY
        {
            get => _fieldY;
            set
            {
                _fieldY = value;
                LocationChanged?.Invoke();
            }
        }

        public void SetFieldPosition(int x, int y)
        {
            _fieldX = x;
            _fieldY = y;
            LocationChanged?.Invoke();
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
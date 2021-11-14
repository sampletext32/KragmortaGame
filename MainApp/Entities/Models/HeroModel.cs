using System;
using MainApp.Entities.Enums;

namespace MainApp.Entities.Models
{
    public class HeroModel
    {
        public string Nickname { get; private set; }

        public int FieldX { get; private set; }

        public int FieldY { get; private set; }

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
        }
    }
}
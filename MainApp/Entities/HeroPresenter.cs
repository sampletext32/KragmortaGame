using System;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities
{
    public class HeroPresenter
    {
        private RectangleShape _rectangle;
        private HeroModel _hero;

        private readonly Font _font;
        
        public readonly int CellSize = 96;
        public readonly int CellMargin = 6;

        public readonly int HeroSize = 64;

        public HeroPresenter(HeroModel hero)
        {
            _hero = hero;
            _font = new Font("assets/fonts/arial.ttf");

            _rectangle = new RectangleShape();

            _rectangle.Position = CalcRectanglePosition();
            _rectangle.Size = new Vector2f(HeroSize, HeroSize);

            _rectangle.FillColor = new Color(255, 255, 255, 100);
        }

        private Vector2f CalcRectanglePosition()
        {
            return new Vector2f(
                (CellSize / 2 - HeroSize / 2) + _hero.FieldX * (CellSize + CellMargin),
                (CellSize / 2 - HeroSize / 2)  + _hero.FieldY * (CellSize + CellMargin)
            );
        }

        public void Render(RenderTarget target)
        {
            target.Draw(_rectangle);
        }

        public void OnHeroMoved()
        {
            _rectangle.Position = CalcRectanglePosition();
        }
    }
}
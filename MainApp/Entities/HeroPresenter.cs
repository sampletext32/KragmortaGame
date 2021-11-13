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

            _hero.LocationChanged += OnHeroMoved;
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
        
        // TODO: Pososi bibu and refactor this repeated (if u r gay -> duplicated) shit here and in GameFieldPresenter. 
        public bool IsMouseWithinBounds(int x, int y)
        {
            // TODO: Add offset of the field (currently at 0,0)
            return !(x >= (CellSize + CellMargin) * (_hero.FieldX + 1) ||
                     y >= (CellSize + CellMargin) * (_hero.FieldY + 1) ||
                     x <= (CellSize + CellMargin) * _hero.FieldX ||
                     y <= (CellSize + CellMargin) * _hero.FieldY);
        }

        /// <summary>
        /// Converts the screen X to field Cell X
        /// </summary>
        public int ConvertMouseXToCellX(int x)
        {
            return x / (CellSize + CellMargin);
        }
        
        /// <summary>
        /// Converts the screen Y to field Cell Y
        /// </summary>
        public int ConvertMouseYToCellY(int y)
        {
            return y / (CellSize + CellMargin);
        }
    }
}
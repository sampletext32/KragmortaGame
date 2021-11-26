using System;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class HeroPresenter : CellPresenterAbstract
    {
        private RectangleShape _rectangle;
        private HeroModel _hero;

        // private readonly Font _font;

        public static readonly int HeroSize = 64;

        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        public HeroPresenter(HeroModel hero)
        {
            _hero = hero;
            // _font = new Font("assets/fonts/arial.ttf");

            _rectangle = new RectangleShape();

            _rectangle.Position = CalcRectanglePosition();
            _rectangle.Size     = new Vector2f(HeroSize, HeroSize);

            _rectangle.FillColor = new Color(
                red: (byte)Rand.Next(0, 256),
                green: (byte)Rand.Next(0, 256),
                blue: (byte)Rand.Next(0, 256),
                alpha: 100
            );
        }

        public override void Render(RenderTarget target)
        {
            if (_hero.Dirty)
            {
                Update();
                _hero.Dirty = false;
            }

            target.Draw(_rectangle);
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return !(
                x - FieldOriginX >= (CellSize + CellMargin) * (_hero.FieldX + 1) ||
                y - FieldOriginY >= (CellSize + CellMargin) * (_hero.FieldY + 1) ||
                x - FieldOriginX <= (CellSize + CellMargin) * _hero.FieldX ||
                y - FieldOriginY <= (CellSize + CellMargin) * _hero.FieldY
            );
        }

        private void Update()
        {
            _rectangle.Position = CalcRectanglePosition();
            if (_hero.IsCurrentHero)
            {
                _rectangle.OutlineThickness = 5;
                _rectangle.OutlineColor     = Color.Red;
            }
            else
            {
                _rectangle.OutlineThickness = 0;
                _rectangle.OutlineColor     = Color.Transparent;
            }
        }

        private Vector2f CalcRectanglePosition()
        {
            return new Vector2f(
                FieldOriginX + (CellSize / 2 - HeroSize / 2) + _hero.FieldX * (CellSize + CellMargin),
                FieldOriginY + (CellSize / 2 - HeroSize / 2) + _hero.FieldY * (CellSize + CellMargin)
            );
        }
    }
}
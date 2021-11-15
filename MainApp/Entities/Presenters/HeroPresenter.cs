using System;
using System.Data;
using MainApp.Entities.Models;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities.Presenters
{
    public class HeroPresenter : CellPresenterAbstract
    {
        private RectangleShape _rectangle;
        private HeroModel _hero;

        private readonly Font _font;

        public static readonly int HeroSize = 64;

        public HeroPresenter(HeroModel hero)
        {
            _hero = hero;
            _font = new Font("assets/fonts/arial.ttf");

            _rectangle = new RectangleShape();

            _rectangle.Position = CalcRectanglePosition();
            _rectangle.Size     = new Vector2f(HeroSize, HeroSize);

            var rand = new Random(DateTime.Now.Millisecond);

            _rectangle.FillColor = new Color((byte)rand.Next(0, 256), (byte)rand.Next(0, 256), (byte)rand.Next(0, 256), 100);
        }

        private Vector2f CalcRectanglePosition()
        {
            return new Vector2f(
                FieldOriginX + (CellSize / 2 - HeroSize / 2) + _hero.FieldX * (CellSize + CellMargin),
                FieldOriginY + (CellSize / 2 - HeroSize / 2) + _hero.FieldY * (CellSize + CellMargin)
            );
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_rectangle);
        }

        public void OnHeroMoved()
        {
            _rectangle.Position = CalcRectanglePosition();
        }

        public void OnHeroActivated()
        {
            _rectangle.OutlineThickness = 5;
            _rectangle.OutlineColor = Color.Red;
        }

        public void OnHeroDeactivated()
        {
            _rectangle.OutlineThickness = 0;
            _rectangle.OutlineColor     = Color.Transparent;
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
    }
}
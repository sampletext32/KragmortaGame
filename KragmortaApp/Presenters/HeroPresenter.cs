using System;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class HeroPresenter : CellPresenterAbstract
    {
        private RectangleShape _rectangle;
        private Text _text;
        private HeroModel _hero;

        // private readonly Font _font;

        public static readonly int HeroSize = 64;

        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        public HeroPresenter(HeroModel hero)
        {
            _hero = hero;

            _rectangle = new RectangleShape();
            _text      = new Text(_hero.Nickname, Engine.Instance.FontCache.GetOrCache("arial"), (uint)(HeroSize / 4));

            _rectangle.Position = CalcRectanglePosition();
            _text.Position = CalcRectanglePosition();
            _rectangle.Size     = new Vector2f(HeroSize, HeroSize);

            _rectangle.FillColor = new Color(
                red: (byte)Rand.Next(100, 256),
                green: (byte)Rand.Next(100, 256),
                blue: (byte)Rand.Next(100, 256),
                alpha: 150
            );
            
            FieldOriginChanged += OnFieldOriginChanged;
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            _rectangle.Position += new Vector2f(x, y);
        }

        public override void Render(RenderTarget target)
        {
            if (_hero.Dirty)
            {
                Update();
                _hero.Dirty = false;
            }

            target.Draw(_rectangle);
            target.Draw(_text);
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
            _text.Position = CalcRectanglePosition();
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
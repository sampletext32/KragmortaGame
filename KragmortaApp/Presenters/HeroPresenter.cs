using System;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class HeroPresenter : CellPresenterAbstract
    {
        private Sprite _rectangle;
        private Text _text;
        private HeroModel _hero;
        private Profile _profile;

        public static readonly int HeroSize = 64;

        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        public HeroPresenter(HeroModel hero, Profile profile)
        {
            _hero    = hero;
            _profile = profile;

            _rectangle = new Sprite();
            _text      = new Text(_profile.Nickname, Engine.Instance.FontCache.GetOrCache("arial"), (uint)(HeroSize / 3.5));

            _rectangle.Texture = InitTexture();
            _rectangle.Position = CalcRectanglePosition();
            var downscaleFactor = (float)HeroSize / _rectangle.Texture.Size.X;
            _rectangle.Scale = new Vector2f(downscaleFactor, downscaleFactor);
            _text.Position   = CalcRectanglePosition();
            
            
            FieldOriginChanged += OnFieldOriginChanged;
        }

        private Texture InitTexture()
        {
            return Engine.Instance.TextureCache.GetOrCache($"goblins/Goblin{_hero.Id - 1}");
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            _rectangle.Position += new Vector2f(x, y);
            _text.Position += new Vector2f(x, y);
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
            _text.Position = CalcTextRectanglePosition();
            // TODO: Wrap into a frame the current hero.
            // if (_hero.IsCurrentHero)
            // {
            //     _rectangle.OutlineThickness = 5;
            //     _rectangle.OutlineColor     = Color.Red;
            // }
            // else
            // {
            //     _rectangle.OutlineThickness = 0;
            //     _rectangle.OutlineColor     = Color.Transparent;
            // }
        }

        private Vector2f CalcRectanglePosition()
        {
            return new Vector2f(
                FieldOriginX + (CellSize / 2 - HeroSize / 2) + _hero.FieldX * (CellSize + CellMargin),
                FieldOriginY + (CellSize / 2 - HeroSize / 2) + _hero.FieldY * (CellSize + CellMargin)
            );
        }

        private Vector2f CalcTextRectanglePosition()
        {
            return new Vector2f(
                FieldOriginX + (CellSize / 2 - HeroSize / 2) + _hero.FieldX * (CellSize + CellMargin) + 3,
                FieldOriginY + (CellSize / 2 - HeroSize / 2) + _hero.FieldY * (CellSize + CellMargin) - 14
            );
        }
    }
}
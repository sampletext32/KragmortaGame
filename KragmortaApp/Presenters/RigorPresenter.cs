using System;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class RigorPresenter : CellPresenterAbstract
    {
        private Sprite _rectangle;
        private RigorModel _rigor;
        
        public static readonly int HeroSize = 64;
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        public RigorPresenter(RigorModel rigor)
        {
            _rigor = rigor;
            
            _rectangle = new Sprite();

            _rectangle.Texture  = InitTexture();
            _rectangle.Position = CalcRectanglePosition();
            var downscaleFactor = (float)HeroSize / _rectangle.Texture.Size.X;
            downscaleFactor  *= 0.94f;
            _rectangle.Scale =  new Vector2f(downscaleFactor, downscaleFactor);
            
            
            FieldOriginChanged += OnFieldOriginChanged;

        }

        private Texture InitTexture()
        {
            return Engine.Instance.TextureCache.GetOrCache($"Mor");
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            _rectangle.Position += new Vector2f(x, y);
        }

        public override void Render(RenderTarget target)
        {
            if (_rigor.Dirty)
            {
                Update();
                _rigor.Dirty = false;
            }

            target.Draw(_rectangle);
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return !(
                x - FieldOriginX >= (CellSize + CellMargin) * (_rigor.FieldX + 1) ||
                y - FieldOriginY >= (CellSize + CellMargin) * (_rigor.FieldY + 1) ||
                x - FieldOriginX <= (CellSize + CellMargin) * _rigor.FieldX ||
                y - FieldOriginY <= (CellSize + CellMargin) * _rigor.FieldY
            );
        }

        private void Update()
        {
            _rectangle.Position = CalcRectanglePosition();
        }

        private Vector2f CalcRectanglePosition()
        {
            return new Vector2f(
                FieldOriginX + (CellSize / 2 - HeroSize / 2) + _rigor.FieldX * (CellSize + CellMargin),
                FieldOriginY + (CellSize / 2 - HeroSize / 2) + _rigor.FieldY * (CellSize + CellMargin) - 24
            );
        }
    }
}
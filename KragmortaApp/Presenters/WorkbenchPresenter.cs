using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class WorkbenchPresenter : CellPresenterAbstract
    {
        private Sprite _backgroundSprite;

        private readonly int _width = 160; 
        private readonly int _height = (int)(160 * 1.28);


        public WorkbenchPresenter()
        {
            _backgroundSprite = new Sprite(Engine.Instance.TextureCache.GetOrCache("table"));
            _backgroundSprite.Scale = new Vector2f((float)_width / _backgroundSprite.Texture.Size.X,
                (float)_height / _backgroundSprite.Texture.Size.Y);
            _backgroundSprite.Position = CalcRectanglePosition();

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private void OnFieldOriginChanged(int x, int y)
        {
            _backgroundSprite.Position += new Vector2f(x, y);
        }

        private Vector2f CalcRectanglePosition()
        {
            return new Vector2f(
                FieldOriginX + (CellSize / 2 - _width / 2) + 40,
                FieldOriginY + (CellSize / 2 - _height / 2) + 8 * (CellSize + CellMargin) + 55
            );
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return false;
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_backgroundSprite);
        }
    }
}
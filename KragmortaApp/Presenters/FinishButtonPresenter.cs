using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace KragmortaApp.Presenters
{
    public class FinishButtonPresenter : Presenter
    {
        private RectangleShape _backgroundRectangle;

        private RectangleShape _button1Rectangle;
        private Text _button1Text;

        private int _x;
        private int _y;
        private int _width = 150;
        private int _height = 80;
        private readonly int _offsetX = 10;
        private readonly int _offsetY = 8;

        public FinishButtonPresenter()
        {
            _backgroundRectangle = new RectangleShape()
            {
                Size      = new Vector2f(_width, _height),
                FillColor = new Color(255, 255, 255)
            };

            _button1Rectangle = new RectangleShape()
            {
                Size             = new Vector2f(_width - _offsetX, _height - _offsetY),
                FillColor        = Color.White,
                OutlineColor     = Color.Black,
                OutlineThickness = 3
            };

            Font font = new Font("assets/fonts/arial.ttf");
            _button1Text = new Text()
            {
                Font            = font,
                DisplayedString = "Finish Turn",
                FillColor       = Color.Black,
                CharacterSize   = 18
            };

            Reshape(Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);
        }

        private void Reshape(int width, int height)
        {
            _x = width / 2 - _width / 2;
            _y = height - _height - 30;

            _backgroundRectangle.Position = new Vector2f(_x, _y);
            _button1Rectangle.Position    = new Vector2f(_x + _offsetX / 2, _y + _offsetY / 2);
            // TODO: Calculate central position of the text.
            _button1Text.Position         = new Vector2f(_x + 40, 
            _y + _height / 2);
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return x >= _x && x <= _x + _width &&
                   y >= _y && y <= _y + _height;
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);
            target.Draw(_button1Rectangle);
            target.Draw(_button1Text);
        }
    }
}
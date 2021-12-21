using System;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.UI
{
    public class UIButton : IUIElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public event Action Clicked;

        public Font TextFont
        {
            get => _text.Font;
            set => _text.Font = value;
        }

        public Color TextColor
        {
            get => _text.FillColor;
            set => _text.FillColor = value;
        }

        public uint TextSize
        {
            get => _text.CharacterSize;
            set => _text.CharacterSize = value;
        }

        private Text _text;

        private RectangleShape _backgroundRectangle;

        private bool _hovered;
        private bool _clicked;

        private static Color BackgroundColor = new Color(255, 255, 255, 100);
        private static Color HoveredColor = new Color(255, 255, 255, 150);
        private static Color ClickedColor = new Color(255, 255, 255, 200);

        public UIButton(int width, int height, string text, Font font)
        {
            Width  = width;
            Height = height;
            _backgroundRectangle = new RectangleShape(new Vector2f(Width, Height))
            {
                FillColor = BackgroundColor
            };
            _text = new Text(text, font, (uint)(Height * 0.8f));
        }

        public void ApplyReflow()
        {
            var textBounds = _text.GetGlobalBounds();
            _backgroundRectangle.Position = new Vector2f(X, Y);
            _text.Position                = new Vector2f(X + Width / 2 - textBounds.Width / 2, Y + Height / 2 - textBounds.Height / 2);
        }

        public void Render(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);
            target.Draw(_text);
        }

        public void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (x >= X && x <= X + Width &&
                y >= Y && y <= Y + Height)
            {
                _clicked                       = true;
                _backgroundRectangle.FillColor = ClickedColor;
            }
        }

        public void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            if (x >= X && x <= X + Width &&
                y >= Y && y <= Y + Height)
            {
                if (_clicked)
                {
                    _clicked = false;
                    Clicked?.Invoke();
                }
            }
            else
            {
                _clicked = false;
            }
            
            if (_hovered)
            {
                _backgroundRectangle.FillColor = HoveredColor;
            }
            else
            {
                _backgroundRectangle.FillColor = BackgroundColor;
            }
        }

        public void OnMouseMoved(int x, int y)
        {
            if (x >= X && x <= X + Width &&
                y >= Y && y <= Y + Height)
            {
                if (!_hovered && !_clicked)
                {
                    _backgroundRectangle.FillColor = HoveredColor;
                    _hovered                       = true;
                }
            }
            else
            {
                if (_hovered && !_clicked)
                {
                    _backgroundRectangle.FillColor = BackgroundColor;
                    _hovered                       = false;
                }
            }
        }
    }
}
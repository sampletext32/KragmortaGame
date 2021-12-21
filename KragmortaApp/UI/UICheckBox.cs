using System;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.UI
{
    public class UICheckBox : IUIElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private int _markSize = 20;
        private RectangleShape _markBackground;
        private RectangleShape _markRect;

        private Text _text;
        
        public bool Checked { get; private set; }

        public event Action<bool> CheckedChanged;

        public UICheckBox(int width, int height, string text, Font font, bool initialState)
        {
            Width   = width;
            Height  = height;
            _text   = new Text(text, font);
            Checked = initialState;

            _markBackground = new RectangleShape()
            {
                Size      = new Vector2f(_markSize, _markSize),
                FillColor = Color.White
            };
            _markRect = new RectangleShape()
            {
                Size      = new Vector2f(_markSize / 2, _markSize / 2),
                FillColor = Color.Black
            };
        }

        public void ApplyReflow()
        {
            _markBackground.Position = new Vector2f(X, Y + _text.CharacterSize / 2 - _markSize / 2);
            _markRect.Position       = new Vector2f(X + _markSize / 4, Y + _text.CharacterSize / 2 - _markSize / 4);
            _text.Position           = new Vector2f(X + _markSize, Y);
        }

        public void Render(RenderTarget target)
        {
            target.Draw(_markBackground);
            if (Checked)
            {
                target.Draw(_markRect);
            }

            target.Draw(_text);
        }

        public void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
        }

        public void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            if (x >= X && x < X + _markSize &&
                y >= Y + _text.CharacterSize / 2 - _markSize / 2 && y < Y + _text.CharacterSize / 2 - _markSize / 2 + _markSize)
            {
                Checked = !Checked;
                CheckedChanged?.Invoke(Checked);
            }
        }

        public void OnMouseMoved(int x, int y)
        {
        }
    }
}
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.UI
{
    public class UIText : IUIElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Text _text;

        public UIText(int width, int height, string text, Font font)
        {
            Width  = width;
            Height = height;
            _text  = new Text(text, font);
        }

        public void ApplyReflow()
        {
            _text.Position = new Vector2f(X, Y);
        }

        public void Render(RenderTarget target)
        {
            target.Draw(_text);
        }

        public void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
        }

        public void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public void OnMouseMoved(int x, int y)
        {
        }
    }
}
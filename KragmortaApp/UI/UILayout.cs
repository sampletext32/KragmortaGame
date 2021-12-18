using System.Collections.Generic;
using SFML.Graphics;

namespace KragmortaApp.UI
{
    public abstract class UILayout : IUIElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        protected readonly List<IUIElement> Elements;

        public UILayout(int x, int y, int width, int height)
        {
            X      = x;
            Y      = y;
            Width  = width;
            Height = height;

            Elements = new();
        }

        public void AddElement(IUIElement element)
        {
            Elements.Add(element);

            Reflow();
        }

        public void ApplyReflow()
        {
            Reflow();
        }

        public void Render(RenderTarget target)
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                Elements[i].Render(target);
            }
        }

        public void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                Elements[i].OnMousePressed(x, y, mouseButton);
            }
        }

        public void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                Elements[i].OnMouseReleased(x, y, mouseButton);
            }
        }

        public void OnMouseMoved(int x, int y)
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                Elements[i].OnMouseMoved(x, y);
            }
        }

        protected abstract void Reflow();
    }
}
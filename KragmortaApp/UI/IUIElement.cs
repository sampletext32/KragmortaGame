using SFML.Graphics;

namespace KragmortaApp.UI
{
    public interface IUIElement
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }

        void ApplyReflow();

        void Render(RenderTarget target);
        void OnMousePressed(int x, int y, KragMouseButton mouseButton);
        void OnMouseReleased(int x, int y, KragMouseButton mouseButton);
        void OnMouseMoved(int x, int y);
    }
}
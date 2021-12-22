using System;
using KragmortaApp.UI;
using SFML.Graphics;

namespace KragmortaApp.Scenes
{
    public class PleaseRestartScene : Scene
    {
        private VerticalLayout _layout;

        public override void OnCreate()
        {
            OnCreateCalled = true;
            _layout        = new(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            Font font = Engine.Instance.FontCache.GetOrCache("arial");
            
            _layout.AddElement(new UIText(300, 30, "Please Restart", font));

            var exitButton = new UIButton(300, 100, "EXIT", font);
            exitButton.Clicked   += ExitButtonOnClicked;
            exitButton.TextColor =  Color.Black;
            exitButton.TextSize  =  32;
            _layout.AddElement(exitButton);
        }

        private void ExitButtonOnClicked()
        {
            Engine.Instance.Window.Close();
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            _layout.Render(target);
        }

        public override void OnMouseMoved(int x, int y)
        {
            _layout.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _layout.OnMousePressed(x, y, mouseButton);
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _layout.OnMouseReleased(x, y, mouseButton);
        }

        public override void OnWindowResized(int width, int height)
        {
            _layout.Width  = width;
            _layout.Height = height;
            _layout.ApplyReflow();
        }

        public override void OnMouseScrolled(int x, int y, bool isVertical, float delta)
        {
        }
    }
}
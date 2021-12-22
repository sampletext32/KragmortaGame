using KragmortaApp.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace KragmortaApp.Scenes
{
    public class PauseMenuScene : Scene
    {
        private RectangleShape _backgroundRectangle;
        private VerticalLayout _layout;
        private int _width = 300;
        private int _height = 150;

        public override void OnCreate()
        {
            _backgroundRectangle = new RectangleShape()
            {
                Size      = new Vector2f(_width, _height),
                FillColor = new Color(0xFF, 0xFF, 0xE0, 30)
            };

            _layout = new VerticalLayout(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            _backgroundRectangle.Position = new Vector2f(Engine.Instance.WindowWidth / 2 - _width / 2, Engine.Instance.WindowHeight / 2 - _height / 2);

            var font = Engine.Instance.FontCache.GetOrCache("arial");

            _layout.AddElement(new UIText(_width, 16, "PAUSED", font));

            var continueButton = new UIButton(300, 26, "Continue", font);
            continueButton.Clicked += ContinueButtonOnClicked;
            _layout.AddElement(continueButton);

            var endButton = new UIButton(300, 26, "Exit", font);
            endButton.Clicked += ExitButtonOnClicked;
            _layout.AddElement(endButton);
        }

        private void ExitButtonOnClicked()
        {
            // Triple pop because of pause scene + table scene + start scene
            Engine.Instance.PopScene();
            Engine.Instance.PopScene();
            Engine.Instance.PopScene();
        }

        private void ContinueButtonOnClicked()
        {
            Engine.Instance.PopScene();
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);
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
            _layout.Width                 = width;
            _layout.Height                = height;
            _backgroundRectangle.Position = new Vector2f(Engine.Instance.WindowWidth / 2 - _width / 2, Engine.Instance.WindowHeight / 2 - _height / 2);
            _layout.ApplyReflow();
        }

        public override void OnMouseScrolled(int x, int y, bool isVertical, float delta)
        {
        }

        public override void OnKeyPressed(Keyboard.Key code)
        {
            if (code == Keyboard.Key.Escape)
            {
                ContinueButtonOnClicked();
            }
        }
    }
}
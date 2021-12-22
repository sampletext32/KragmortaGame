using System;
using KragmortaApp.UI;
using SFML.Graphics;

namespace KragmortaApp.Scenes
{
    public class MenuScene : Scene
    {
        private VerticalLayout _layout;

        public override void OnCreate()
        {
            OnCreateCalled = true;
            _layout        = new(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            Font font = Engine.Instance.FontCache.GetOrCache("arial");

            var startButton = new UIButton(300, 100, "START", font);
            startButton.Clicked   += StartButtonOnClicked;
            startButton.TextColor =  Color.Black;
            startButton.TextSize  =  32;
            _layout.AddElement(startButton);

            var rulesButton = new UIButton(300, 100, "RULES", font);
            rulesButton.Clicked   += RulesButtonOnClicked;
            rulesButton.TextColor =  Color.Black;
            rulesButton.TextSize  =  32;
            _layout.AddElement(rulesButton);

            var settingsButton = new UIButton(300, 100, "SETTINGS", font);
            settingsButton.Clicked   += SettingsButtonOnClicked;
            settingsButton.TextColor =  Color.Black;
            settingsButton.TextSize  =  32;
            _layout.AddElement(settingsButton);

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

        private void SettingsButtonOnClicked()
        {
            Engine.Instance.PushScene(new SettingsScene());
        }

        private void RulesButtonOnClicked()
        {
        }

        private void StartButtonOnClicked()
        {
            Engine.Instance.PushScene(new GameStartScene());
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
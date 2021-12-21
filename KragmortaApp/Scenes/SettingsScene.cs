using System;
using KragmortaApp.UI;
using SFML.Graphics;

namespace KragmortaApp.Scenes
{
    public class SettingsScene : Scene
    {
        private VerticalLayout _layout;

        public override void OnCreate()
        {
            _layout = new(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            Font font = new Font("assets/fonts/arial.ttf");
            _layout.AddElement(new UIText(300, 50, "SETTINGS", font));

            var soundsCheckBox = new UICheckBox(300, 50, "Enable Sounds", font, false);
            soundsCheckBox.CheckedChanged += SoundsCheckBoxOnCheckedChanged;
            _layout.AddElement(soundsCheckBox);

            var backButton = new UIButton(300, 50, "Back", font);
            backButton.Clicked   += BackButtonOnClicked;
            backButton.TextColor =  Color.Black;
            backButton.TextSize  =  32;
            _layout.AddElement(backButton);

            _layout.ApplyReflow();
        }

        private void SoundsCheckBoxOnCheckedChanged(bool state)
        {
            Console.WriteLine($"Sounds set to {state}");
        }

        private void BackButtonOnClicked()
        {
            Engine.Instance.PopScene();
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
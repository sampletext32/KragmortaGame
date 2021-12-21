using System;
using KragmortaApp.UI;
using SFML.Graphics;

namespace KragmortaApp.Scenes
{
    public class SettingsScene : Scene
    {
        private VerticalLayout _layout;

        private static string[] _resolutions =
        {
            "800x600",
            "1200x800",
            "1280x720",
            "1600x900",
            "1920x1080",
        };

        private int _selectedResolution;
        private int _currentResolution;
        private UIText _resolutionText;

        public override void OnCreate()
        {
            _layout = new(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            Font font = new Font("assets/fonts/arial.ttf");
            _layout.AddElement(new UIText(300, 50, "SETTINGS", font));

            var soundsCheckBox = new UICheckBox(300, 30, "Enable Sounds", font, Engine.Instance.Settings.EnableSounds);
            soundsCheckBox.CheckedChanged += SoundsCheckBoxOnCheckedChanged;
            _layout.AddElement(soundsCheckBox);

            var fullscreenCheckBox = new UICheckBox(300, 30, "Enable Fullscreen", font, Engine.Instance.Settings.FullScreen);
            fullscreenCheckBox.CheckedChanged += FullScreenCheckBoxOnCheckedChanged;
            _layout.AddElement(fullscreenCheckBox);

            _layout.AddElement(new UIText(300, 30, "Resolution", font));

            _resolutionText = new UIText(300, 16, $"Current: {Engine.Instance.WindowWidth}x{Engine.Instance.WindowHeight}", font);
            _layout.AddElement(_resolutionText);

            _currentResolution  = _resolutions.AsSpan().IndexOf($"{Engine.Instance.WindowWidth}x{Engine.Instance.WindowHeight}");
            _selectedResolution = _currentResolution;
            var sliderResolution = new UISlider(300, 30, _resolutions.Length, _currentResolution);
            sliderResolution.StepChanged += SliderOnStepChanged;
            _layout.AddElement(sliderResolution);

            var apply = new UIButton(300, 30, "Apply", font);
            apply.Clicked   += ApplyButtonOnClicked;
            apply.TextColor =  Color.Black;
            apply.TextSize  =  16;
            _layout.AddElement(apply);

            var backButton = new UIButton(300, 50, "Back", font);
            backButton.Clicked   += BackButtonOnClicked;
            backButton.TextColor =  Color.Black;
            backButton.TextSize  =  26;
            _layout.AddElement(backButton);

            _layout.ApplyReflow();
        }

        private void FullScreenCheckBoxOnCheckedChanged(bool state)
        {
            Engine.Instance.Settings.FullScreen = state;
        }

        private void ApplyButtonOnClicked()
        {
            if (_currentResolution != _selectedResolution)
            {
                var resolution = _resolutions[_selectedResolution];
                var resParts   = resolution.Split('x');
                int width      = Convert.ToInt32(resParts[0]);
                int height     = Convert.ToInt32(resParts[1]);
                Engine.Instance.SetWindowSize(width, height);
                _resolutionText.SetText($"Current: {Engine.Instance.WindowWidth}x{Engine.Instance.WindowHeight}");
                _currentResolution = _selectedResolution;

                Engine.Instance.Settings.ResolutionWidth  = width;
                Engine.Instance.Settings.ResolutionHeight = height;
            }
            Engine.Instance.Settings.Save();
        }

        private void SliderOnStepChanged(int step)
        {
            if (step != _currentResolution)
            {
                var resolution = _resolutions[step];
                var resParts   = resolution.Split('x');
                int width      = Convert.ToInt32(resParts[0]);
                int height     = Convert.ToInt32(resParts[1]);
                // Console.WriteLine($"New resolution is {resolution}");
                _resolutionText.SetText($"Current: {Engine.Instance.WindowWidth}x{Engine.Instance.WindowHeight}, Selected: {width}x{height}");
                _selectedResolution = step;
            }
            else
            {
                _resolutionText.SetText($"Current: {Engine.Instance.WindowWidth}x{Engine.Instance.WindowHeight}");
            }
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
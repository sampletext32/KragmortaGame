using KragmortaApp.UI;
using SFML.Graphics;
using SFML.Window;

namespace KragmortaApp.Scenes
{
    public class GameStartScene : Scene
    {
        private VerticalLayout _layout;

        private UIText _playerCountText;

        private int _playersCount = 2;

        public override void OnCreate()
        {
            OnCreateCalled = true;

            _layout = new(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            Font font = Engine.Instance.FontCache.GetOrCache("arial");

            _layout.AddElement(new UIText(300, 30, "Start New Game", font));
            _layout.AddElement(new UIText(300, 16, "Players Count", font));

            _playerCountText = new UIText(300, 16, $"Current: {_playersCount}", font);
            _layout.AddElement(_playerCountText);

            var playersCountSlider = new UISlider(300, 20, 7, 0);
            playersCountSlider.StepChanged += PlayersCountSliderOnStepChanged;
            _layout.AddElement(playersCountSlider);

            var startButton = new UIButton(300, 100, "START", font);
            startButton.Clicked   += StartButtonOnClicked;
            startButton.TextColor =  Color.Black;
            startButton.TextSize  =  32;
            _layout.AddElement(startButton);
        }

        private void PlayersCountSliderOnStepChanged(int step)
        {
            _playersCount = step + 2;
            _playerCountText.SetText($"Current: {_playersCount}");
        }

        private void StartButtonOnClicked()
        {
            GameState.InitForPlayers(_playersCount);
            Engine.Instance.PushScene(new GameTableScene());
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

        public override void OnKeyPressed(Keyboard.Key code)
        {
        }
    }
}
using System.Linq;
using KragmortaApp.UI;
using SFML.Graphics;
using SFML.Window;

namespace KragmortaApp.Scenes
{
    public class GameOverScene : Scene
    {
        private VerticalLayout _layout;


        public override void OnCreate()
        {
            OnCreateCalled = true;

            _layout = new(0, 0, Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);

            var font = Engine.Instance.FontCache.GetOrCache("arial");

            var sortedProfiles = GameState.Instance.Profiles
                .OrderByDescending(p => p.MagicBooks.Count)
                .ThenByDescending(p => p.MagicBooks.Sum(b => b.Power))
                .ThenByDescending(p => p.Lives)
                .ToList();

            var divider = new string('-', 30);

            for (var i = 0; i < sortedProfiles.Count; i++)
            {
                _layout.AddElement(new UIText(800, 30,
                    $"{i + 1}: {sortedProfiles[i].Nickname}. " +
                    $"MB_Count = {sortedProfiles[i].MagicBooks.Count}. " +
                    $"TP = {sortedProfiles[i].MagicBooks.Sum(b => b.Power)}. " +
                    $"Lives = {sortedProfiles[i].Lives}",
                    font));

                _layout.AddElement(new UIText(800, 30,
                    divider,
                    font));
            }
            
            var exitButton = new UIButton(300, 100, "Exit", font);
            exitButton.Clicked   += ExitButtonOnClicked;
            exitButton.TextColor =  Color.Black;
            exitButton.TextSize  =  32;
            
            _layout.AddElement(exitButton);

            _layout.ApplyReflow();
        }

        private void ExitButtonOnClicked()
        {
            Engine.Instance.PopScene();
            Engine.Instance.PopScene();
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

        public override void OnKeyPressed(Keyboard.Key code)
        {
        }
    }
}
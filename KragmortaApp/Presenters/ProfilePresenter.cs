using System;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class ProfilePresenter : Presenter
    {
        private Text _text;
        private RectangleShape _rectangle;
        private Profile _profile;
        private Corner _corner;

        private readonly Font _font;

        public ProfilePresenter(Profile profile, Corner corner)
        {
            _profile = profile;
            _corner  = corner;
            _font    = new Font("assets/fonts/arial.ttf");

            _text      = new Text();
            _rectangle = new RectangleShape();
            _text.Font = _font;

            _rectangle.FillColor = new Color(255, 255, 255, 100);

            _text.DisplayedString = _profile.Nickname;
            _text.CharacterSize   = 24;
            _text.FillColor       = Color.Black;

            Reshape(Game.Instance.WindowWidth, Game.Instance.WindowHeight);
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_rectangle);
            target.Draw(_text);
        }

        public override void OnWindowResized(int width, int height)
        {
            Reshape(width, height);
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return false;
        }

        private void Reshape(int width, int height)
        {
            int rectWidth  = 200;
            int rectHeight = 50;

            switch (_corner)
            {
                case Corner.TopLeft:
                    _rectangle.Position = new Vector2f(0, 0);
                    _text.Position      = new Vector2f(0, (rectHeight - _text.CharacterSize) / 2f);
                    break;
                case Corner.TopRight:
                    _rectangle.Position = new Vector2f(width - rectWidth, 0);
                    _text.Position      = new Vector2f(width - rectWidth, (rectHeight - _text.CharacterSize) / 2f);
                    break;
                case Corner.BottomLeft:
                    _rectangle.Position = new Vector2f(0, height - rectHeight);
                    _text.Position      = new Vector2f(0, height - rectHeight + (rectHeight - _text.CharacterSize) / 2f);
                    break;
                case Corner.BottomRight:
                    _rectangle.Position = new Vector2f(width - rectWidth, height - rectHeight);
                    _text.Position      = new Vector2f(width - rectWidth, height - rectHeight + (rectHeight - _text.CharacterSize) / 2f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _rectangle.Size = new Vector2f(rectWidth, rectHeight);
        }
    }
}
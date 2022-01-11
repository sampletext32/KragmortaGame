using System.Linq;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class ProfileDrawable : Drawable
    {
        public const int BackgroundWidth = 300;
        public const int BackgroundHeight = 90;

        private Text _nicknameText;
        private Text _livesText;
        private Text _booksText;

        private RectangleShape _rectangle;
        private Profile _profile;

        private readonly Font _font;
        private bool _visible;

        public ProfileDrawable(Profile profile)
        {
            _profile = profile;

            _font = Engine.Instance.FontCache.GetOrCache("arial");

            _nicknameText = new Text();
            _livesText    = new Text();
            _booksText    = new Text();
            _rectangle = new RectangleShape()
            {
                Size = new Vector2f(BackgroundWidth, BackgroundHeight)
            };
            _nicknameText.Font = _font;
            _livesText.Font    = _font;
            _booksText.Font    = _font;

            _rectangle.FillColor = new Color(255, 255, 255, 100);

            _nicknameText.CharacterSize = 24;
            _nicknameText.FillColor     = Color.Black;

            _livesText.CharacterSize = 24;
            _livesText.FillColor     = Color.Black;

            _booksText.CharacterSize = 24;
            _booksText.FillColor     = Color.Black;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_profile.Dirty)
            {
                Update();
                _profile.ClearDirty();
            }

            if (!_visible)
            {
                return;
            }

            target.Draw(_rectangle);
            target.Draw(_nicknameText);
            target.Draw(_booksText);
            target.Draw(_livesText);
        }

        public void SetPosition(int x, int y)
        {
            _rectangle.Position    = new Vector2f(x, y);
            _nicknameText.Position = new Vector2f(x, y);
            _booksText.Position    = new Vector2f(x, y + _nicknameText.CharacterSize);
            _livesText.Position    = new Vector2f(x, y + _nicknameText.CharacterSize + _booksText.CharacterSize);
        }

        private void Update()
        {
            _visible                      = _profile.Activated;
            _nicknameText.DisplayedString = _profile.Nickname;
            _booksText.DisplayedString    = $"Books - {_profile.MagicBooks.Count}:{_profile.MagicBooks.Sum(b => b.Power)}";
            _livesText.DisplayedString    = $"Lives - {_profile.Lives}";
        }
    }
}
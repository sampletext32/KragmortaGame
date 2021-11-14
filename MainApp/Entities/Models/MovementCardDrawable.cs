using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities.Models
{
    public class MovementCardDrawable : Drawable
    {
        private readonly MovementCard _movementCard;

        private Font _font;

        private Text _firstText;
        private Text _secondText;

        private RectangleShape _backgroundRectangle;

        private int _fontHeight = 24;

        public static readonly int Width = 130;
        public static readonly int Height = 180;

        public MovementCardDrawable(MovementCard movementCard)
        {
            _movementCard = movementCard;

            _backgroundRectangle           = new RectangleShape();
            _backgroundRectangle.Size      = new Vector2f(Width, Height);
            _backgroundRectangle.FillColor = new Color(255, 255, 255, 100);

            _font = new Font("assets/fonts/arial.ttf");

            _firstText  = new Text();
            _secondText = new Text();

            _firstText.Font  = _font;
            _secondText.Font = _font;

            _firstText.CharacterSize  = (uint)_fontHeight;
            _secondText.CharacterSize = (uint)_fontHeight;
            _firstText.FillColor      = Color.Black;
            _secondText.FillColor     = Color.Black;

            _firstText.DisplayedString  = _movementCard.FirstType.ToString();
            _secondText.DisplayedString = _movementCard.SecondType.ToString();
        }

        public void SetPosition(int x, int y)
        {
            _backgroundRectangle.Position = new Vector2f(x, y);

            _firstText.Position  = new Vector2f(x, y);
            _secondText.Position = new Vector2f(x, y + _fontHeight * 1.5f);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_backgroundRectangle, states);
            target.Draw(_firstText, states);
            target.Draw(_secondText, states);
        }
    }
}
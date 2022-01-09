using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class MovementCardDrawable : Drawable
    {
        private MovementCard _movementCard;

        private Font _font;

        private Text _firstText;
        private Text _secondText;

        private RectangleShape _backgroundRectangle;

        private Sprite _backgroundSprite;
        private RenderTexture _backgroundGoblinRenderTexture;

        private int _fontHeight = 24;

        private int _x;
        private int _y;
        public static readonly int Width = 130;
        public static readonly int Height = 180;

        public MovementCardDrawable()
        {
            _backgroundRectangle           = new RectangleShape();
            _backgroundRectangle.Size      = new Vector2f(Width, Height);
            _backgroundRectangle.FillColor = new Color(255, 255, 255, 100);

            _font = Engine.Instance.FontCache.GetOrCache("arial");

            _firstText  = new Text();
            _secondText = new Text();

            _firstText.Font  = _font;
            _secondText.Font = _font;

            _firstText.CharacterSize  = (uint)_fontHeight;
            _secondText.CharacterSize = (uint)_fontHeight;
            _firstText.FillColor      = Color.Black;
            _secondText.FillColor     = Color.Black;


            var goblinCard = Engine.Instance.TextureCache.GetOrCache("movement/goblin-move");
            _backgroundSprite       = new Sprite(goblinCard);
            _backgroundSprite.Scale = new Vector2f((float)Width / _backgroundSprite.Texture.Size.X,
                (float)Height / _backgroundSprite.Texture.Size.Y);
            // _backgroundGoblinRenderTexture = new RenderTexture(goblinCard.Texture.Size.X, goblinCard.Texture.Size.Y);
            // _backgroundGoblinRenderTexture.Draw(goblinCard);
            // _backgroundGoblinRenderTexture.Draw();

        }

        public void SetCard(MovementCard card)
        {
            _movementCard = card;

            if (card == null)
            {
                return;
            }

            _firstText.DisplayedString  = _movementCard.FirstType.ToString();
            _secondText.DisplayedString = _movementCard.SecondType.ToString();
            Update();
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;

            _backgroundRectangle.Position = new Vector2f(x, y);
            _backgroundSprite.Position    = new Vector2f(x, y);

            _firstText.Position  = new Vector2f(x, y);
            _secondText.Position = new Vector2f(x, y + _fontHeight * 1.5f);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_movementCard is null)
            {
                return;
            }

            if (_movementCard.Dirty)
            {
                Update();
                _movementCard.ClearDirty();
            }

            // target.Draw(_backgroundRectangle, states);
            target.Draw(_backgroundSprite, states);
            target.Draw(_firstText, states);
            target.Draw(_secondText, states);
        }

        private void Update()
        {
            if (_movementCard.Activated)
            {
                _backgroundRectangle.FillColor = new Color(255, 0, 0);
            }
            else if (_movementCard.Selected)
            {
                _backgroundRectangle.FillColor = new Color(255, 0, 0, 100);
            }
            else
            {
                _backgroundRectangle.FillColor = new Color(255, 255, 255, 100);
            }
        }

        public bool TryGetCardFromMousePosition(int x, int y, out MovementCard card)
        {
            if (x < _x ||
                x >= _x + Width ||
                y < _y ||
                y >= _y + Height
            )
            {
                card = null;
                return false;
            }
            else
            {
                card = _movementCard;
                return true;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using MainApp.Entities.Models;
using SFML.Graphics;
using SFML.System;

namespace MainApp.Entities.Presenters
{
    public class MovementDeckPresenter : Presenter
    {
        private MovementDeck _deck;

        private RectangleShape _backgroundRectangle;

        private List<MovementCardDrawable> _drawables;

        private int _width = 500;
        private int _height = 200;

        private int CardsMargin = 10;

        private int CardsTotalWidth => _drawables.Count * MovementCardDrawable.Width;

        private int _x;
        private int _y;

        public MovementDeckPresenter()
        {
            _drawables = new List<MovementCardDrawable>();

            _backgroundRectangle = new RectangleShape();

            _backgroundRectangle.Size      = new Vector2f(_width, _height);
            _backgroundRectangle.FillColor = new Color(255, 255, 255, 150);

            Reshape(Game.Instance.WindowWidth, Game.Instance.WindowHeight);
        }

        public void SetDeck(MovementDeck deck)
        {
            _deck = deck;
            _drawables.Clear();
            _drawables.AddRange(deck.MovementCards
                .Select(c => new MovementCardDrawable(c))
                .ToList());
            Reshape(Game.Instance.WindowWidth, Game.Instance.WindowHeight);
        }

        private void Reshape(int width, int height)
        {
            _x = width / 2 - _width / 2;
            _y = height - _height;

            _backgroundRectangle.Position = new Vector2f(_x, _y);

            int startX = _x + _width / 2 - CardsTotalWidth / 2 - ((_drawables.Count - 1) * CardsMargin / 2);
            int startY = _y + _height / 2 - MovementCardDrawable.Height / 2;

            for (var i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].SetPosition(startX, startY);
                startX += MovementCardDrawable.Width + CardsMargin;
            }
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return x >= _x && x < _x + _width && y >= _y && y < _y + _height;
        }

        public override void OnWindowResized(int width, int height)
        {
            Reshape(width, height);
        }

        public void UpdateCardAtPosition(int i)
        {
            _drawables[i].SetFromCard();
        }

        public int GetCardIndex(int x, int y)
        {
            int startX = _x + _width / 2 - CardsTotalWidth / 2 - ((_drawables.Count - 1) * CardsMargin / 2);
            int startY = _y + _height / 2 - MovementCardDrawable.Height / 2;

            for (var i = 0; i < _drawables.Count; i++)
            {
                if (x > startX && x <= startX + MovementCardDrawable.Width &&
                    y > startY && y <= startY + MovementCardDrawable.Height)
                {
                    return i;
                }

                startX += MovementCardDrawable.Width;
            }

            return -1;
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);

            for (var i = 0; i < _drawables.Count; i++)
            {
                target.Draw(_drawables[i]);
            }
        }

        public void RemoveCardAtPosition(int index)
        {
            _drawables.RemoveAt(index);
            Reshape(Game.Instance.WindowWidth, Game.Instance.WindowHeight);
        }
    }
}
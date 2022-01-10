using System.Collections.Generic;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Drawables
{
    public class MovementDeckDrawable : Drawable
    {
        private MovementDeck _deck;

        private List<MovementCardDrawable> _drawables;

        private bool _visible;

        private int _x;
        private int _y;

        private int _width = 500;
        private int _height = 200;

        private int CardsMargin = 10;

        private int CardsTotalWidth => _drawables.Count * MovementCardDrawable.Width;

        public MovementDeckDrawable(MovementDeck deck, int width, int height)
        {
            _deck   = deck;
            _width  = width;
            _height = height;

            _drawables = new List<MovementCardDrawable>(3);
            
            // NOTE: we need to manually update, so drawables are created and positions are taken into account
            Update();
        }

        public bool TryGetCardFromMousePosition(int x, int y, out MovementCard card)
        {
            if (!_visible)
            {
                card = null;
                return false;
            }

            for (var i = 0; i < _drawables.Count; i++)
            {
                if (_drawables[i].TryGetCardFromMousePosition(x, y, out card))
                    return true;
            }

            card = null;
            return false;
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
            
            int startX = _x + _width / 2 - CardsTotalWidth / 2 - ((_drawables.Count - 1) * CardsMargin / 2);
            int startY = _y + _height / 2 - MovementCardDrawable.Height / 2;

            for (var i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].SetPosition(startX, startY);
                startX += MovementCardDrawable.Width + CardsMargin;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_deck.Dirty)
            {
                Update();
                _deck.ClearDirty();
            }

            if (!_visible) return;
            for (var i = 0; i < _drawables.Count; i++)
            {
                target.Draw(_drawables[i]);
            }
        }

        private void Update()
        {
            _visible = _deck.Visible;
            // TODO: Bind model to drawable from the beginning.

            // Add drawables, if there are not enough of them
            for (int i = _drawables.Count; i < _deck.MovementCards.Count; i++)
            {
                _drawables.Add(new MovementCardDrawable());
            }

            // Set cards to drawables
            for (var i = 0; i < _deck.MovementCards.Count; i++)
            {
                _drawables[i].SetCard(_deck.MovementCards[i]);
            }

            // Set nulls for empty drawables from the back
            for (var i = _deck.MovementCards.Count; i < _drawables.Count; i++)
            {
                _drawables[i].SetCard(null);
            }
        }
    }
}
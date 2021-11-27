﻿using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
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

            Reshape(Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);
        }

        public void SetDeck(MovementDeck deck)
        {
            _deck = deck;
            _deck.MarkDirty();
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);

            if (_deck is null)
            {
                return;
            }

            if (_deck.Dirty)
            {
                Update();
                _deck.ClearDirty();
            }

            for (var i = 0; i < _drawables.Count; i++)
            {
                target.Draw(_drawables[i]);
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

        public bool TryGetCardFromMousePosition(int x, int y, out MovementCard card)
        {
            int startX = _x + _width / 2 - CardsTotalWidth / 2 - ((_deck.MovementCards.Count - 1) * CardsMargin / 2);
            int startY = _y + _height / 2 - MovementCardDrawable.Height / 2;

            for (var i = 0; i < _deck.MovementCards.Count; i++)
            {
                if (x > startX && x <= startX + MovementCardDrawable.Width &&
                    y > startY && y <= startY + MovementCardDrawable.Height)
                {
                    card = _deck.MovementCards[i];
                    return true;
                }

                startX += MovementCardDrawable.Width;
            }

            card = null;
            return false;
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

        private void Update()
        {
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

            Reshape(Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);
        }
    }
}
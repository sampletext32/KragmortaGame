using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class MovementDecksPresenter : Presenter
    {
        private RectangleShape _backgroundRectangle;

        private List<MovementDeckDrawable> _drawables;

        private int _x;
        private int _y;
        private int _width = 500;
        private int _height = 200;

        public MovementDecksPresenter(List<MovementDeck> decks)
        {
            _drawables = new List<MovementDeckDrawable>(decks.Count);
            _drawables.AddRange(decks.Select(d => new MovementDeckDrawable(d, _width, _height)));

            _backgroundRectangle = new RectangleShape();

            _backgroundRectangle.Size      = new Vector2f(_width, _height);
            _backgroundRectangle.FillColor = new Color(255, 255, 255, 150);

            Reshape(Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);

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
            for (var i = 0; i < _drawables.Count; i++)
            {
                if (_drawables[i].TryGetCardFromMousePosition(x, y, out card))
                {
                    return true;
                }
            }

            card = null;
            return false;
        }

        private void Reshape(int width, int height)
        {
            _x = width / 2 - _width / 2;
            _y = height - _height;
            
            for (var i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].SetPosition(_x, _y);
            }

            _backgroundRectangle.Position = new Vector2f(_x, _y);
        }
    }
}
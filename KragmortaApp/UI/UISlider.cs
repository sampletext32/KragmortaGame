using System;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.UI
{
    public class UISlider : IUIElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private RectangleShape _backgroundRectangle;
        private RectangleShape _controlRect;

        private static Color BackgroundColor = new Color(255, 255, 255, 100);
        private static Color ControlColor = new Color(255, 255, 255, 255);

        private float _backgroundLineHeight = 10;
        private float _controlWidth = 10;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                float valPerStep = 1f / (_steps - 1);

                int step = (int)(value / valPerStep);
                if (_previousStep != step)
                {
                    StepChanged?.Invoke(step);
                    _previousStep = step;
                }
            }
        }

        public event Action<int> StepChanged;

        private int _steps;
        private int _previousStep = 0;

        public UISlider(int width, int height, int steps, int initialStep = 0)
        {
            Width         = width;
            Height        = height;
            _steps        = steps;
            _previousStep = initialStep;
            Value         = (float)(initialStep) / (_steps - 1);
            _backgroundRectangle = new RectangleShape(new Vector2f(Width, _backgroundLineHeight))
            {
                FillColor = BackgroundColor
            };
            _controlRect = new RectangleShape(new Vector2f(_controlWidth, Height))
            {
                FillColor = ControlColor
            };
        }

        public void ApplyReflow()
        {
            _backgroundRectangle.Position = new Vector2f(X, Y + Height / 2 - _backgroundLineHeight / 2);
            _controlRect.Position         = new Vector2f(X + Width * Value - _controlWidth / 2, Y);
        }

        public void Render(RenderTarget target)
        {
            target.Draw(_backgroundRectangle);
            target.Draw(_controlRect);
        }

        private bool _dragging;
        private float _value;

        public void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            if (x >= X && x <= X + Width &&
                y >= Y && y <= Y + Height)
            {
                var newValue = (float)(x - X) / Width;
                Value     = newValue;
                _dragging = true;
            }
        }

        public void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            if (x >= X && x <= X + Width &&
                y >= Y && y <= Y + Height)
            {
                var newValue = (float)(x - X) / Width;
                Value                 = newValue;
                _controlRect.Position = new Vector2f(X + Width * Value - _controlWidth / 2, Y);
            }

            _dragging = false;
        }

        public void OnMouseMoved(int x, int y)
        {
            if (_dragging)
            {
                if (x < X)
                {
                    var newValue = 0f;
                    Value                 = newValue;
                    _controlRect.Position = new Vector2f(X + Width * Value - _controlWidth / 2, Y);
                }
                else if (x > X + Width)
                {
                    var newValue = 1f;
                    Value                 = newValue;
                    _controlRect.Position = new Vector2f(X + Width * Value - _controlWidth / 2, Y);
                }
                else
                {
                    var newValue = (float)(x - X) / Width;

                    Value                 = newValue;
                    _controlRect.Position = new Vector2f(X + Width * Value - _controlWidth / 2, Y);
                }
            }
        }
    }
}
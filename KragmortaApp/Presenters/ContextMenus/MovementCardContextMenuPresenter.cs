using KragmortaApp.Entities.ContextMenus;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters.ContextMenus
{
    public class MovementCardContextMenuPresenter : Presenter
    {
        private MovementCardContextMenuModel _movementCardContextMenuModel;

        private RectangleShape _backgroundRectangle;

        private RectangleShape _button1Rectangle;
        private Text _button1Text;

        private bool _visible;

        public MovementCardContextMenuPresenter(MovementCardContextMenuModel movementCardContextMenuModel)
        {
            _movementCardContextMenuModel = movementCardContextMenuModel;

            _backgroundRectangle = new RectangleShape()
            {
                Size      = new Vector2f(150, 40),
                FillColor = new Color(255, 255, 255)
            };

            _button1Rectangle = new RectangleShape()
            {
                Size             = new Vector2f(140, 32),
                FillColor        = Color.Red,
                OutlineColor     = Color.Black,
                OutlineThickness = 3
            };

            Font font = new Font("assets/fonts/arial.ttf");
            _button1Text = new Text()
            {
                Font            = font,
                DisplayedString = "Delete card",
                FillColor       = Color.White,
                CharacterSize   = 18
            };
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return _movementCardContextMenuModel.Visible;
        }

        public bool IsMouseOverContextMenu(int x, int y)
        {
            return x >= _movementCardContextMenuModel.X && x <= _movementCardContextMenuModel.X + 150 &&
                   y >= _movementCardContextMenuModel.Y && y <= _movementCardContextMenuModel.Y + 40;
        }

        public override void Render(RenderTarget target)
        {
            if (_movementCardContextMenuModel.Dirty)
            {
                Update();
            }

            if (!_visible) return;
            target.Draw(_backgroundRectangle);
            target.Draw(_button1Rectangle);
            target.Draw(_button1Text);
        }

        private void Update()
        {
            _visible = _movementCardContextMenuModel.Visible;

            _backgroundRectangle.Position = new(_movementCardContextMenuModel.X, _movementCardContextMenuModel.Y);
            _button1Rectangle.Position    = new(_movementCardContextMenuModel.X + 5, _movementCardContextMenuModel.Y + 4);

            var textBounds = _button1Text.GetLocalBounds();
            _button1Text.Position = new(
                _movementCardContextMenuModel.X + 5 + 140 / 2 - textBounds.Width / 2,
                _movementCardContextMenuModel.Y + 4 + 32 / 2 - _button1Text.CharacterSize / 2);
        }
    }
}
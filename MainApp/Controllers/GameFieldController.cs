using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class GameFieldController
    {
        private FieldCell _lastMouseOverCell = null;
        private FieldCell _lastMouseDownOverCell = null;

        private readonly GameField _field;
        private readonly GameFieldPresenter _fieldPresenter;

        public GameFieldController(GameField field, GameFieldPresenter fieldPresenter)
        {
            _field = field;
            _fieldPresenter = fieldPresenter;
        }

        public CellType GetFieldType(int cX, int cY)
        {
            return _field.GetCell(cX, cY).Type;
        }
        
        public void OnMouseMoved(int x, int y)
        {
            if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                if (_lastMouseOverCell is not null)
                {
                    _lastMouseOverCell.Hovered = false;
                    _lastMouseOverCell         = null;
                }

                return;
            }

            int cX = _fieldPresenter.ConvertMouseXToCellX(x);
            int cY = _fieldPresenter.ConvertMouseYToCellY(y);

            if (_lastMouseOverCell is not null)
            {
                _lastMouseOverCell.Hovered = false;
            }

            var fieldCell = _field.GetCell(cX, cY);
            fieldCell.Hovered  = true;
            _lastMouseOverCell = fieldCell;
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            var cY = _fieldPresenter.ConvertMouseYToCellY(y);

            var fieldCell = _field.GetCell(cX, cY);
            fieldCell.Clicked      = true;
            _lastMouseDownOverCell = fieldCell;
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            // this small check is needed for mouse release not over initially clicked cell (click and drag behavior)
            if (_lastMouseDownOverCell is not null)
            {
                _lastMouseDownOverCell.Clicked = false;
                _lastMouseDownOverCell         = null;
            }

            if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                return;
            }

            var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            var cY = _fieldPresenter.ConvertMouseYToCellY(y);

            var fieldCell = _field.GetCell(cX, cY);
            fieldCell.Clicked = false;
        }

    }
}
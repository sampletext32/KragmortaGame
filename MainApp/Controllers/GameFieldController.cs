using System;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class GameFieldController
    {
        private FieldCell _lastMouseOverCell = null;
        private int _lastMouseOverCellIndex = -1;
        private FieldCell _lastMouseDownOverCell = null;
        private int _lastMouseDownOverCellIndex = -1;

        private readonly GameField _field;
        private readonly GameFieldPresenter _fieldPresenter;

        public GameFieldController(GameField field, GameFieldPresenter fieldPresenter)
        {
            _field          = field;
            _fieldPresenter = fieldPresenter;
        }

        /// <summary>
        /// Returns the CellType of a cell by the passed coordinates x and y.
        /// <remarks>Throws an ArgumentOutOfRangeException if the cX or cY is out of range.</remarks>
        /// </summary>
        /// <param name="cX">Coordinate x of the seeking cell.</param>
        /// <param name="cY">Coordinate y of the seeking cell.</param>
        public CellType GetCellType(int cX, int cY)
        {
            if (cX < 0 || _field.SizeX <= cX)
            {
                throw new ArgumentOutOfRangeException($"Parameter cX is out of range: {cX}");
            }
            if (cY < 0 || _field.SizeY <= cY)
            {
                throw new ArgumentOutOfRangeException($"Parameter cY is out of range: {cY}");
            }
            
            return _field.GetCell(cX, cY).Type;
        }

        public void OnMouseMoved(int x, int y)
        {
            if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                if (_lastMouseOverCell is not null)
                {
                    _lastMouseOverCell.Hovered = false;
                    _fieldPresenter.UpdateCell(_lastMouseOverCell);
                    _lastMouseOverCell = null;
                }

                return;
            }

            int cX = _fieldPresenter.ConvertMouseXToCellX(x);
            int cY = _fieldPresenter.ConvertMouseYToCellY(y);

            if (_lastMouseOverCell is not null)
            {
                _lastMouseOverCell.Hovered = false;
                _fieldPresenter.UpdateCell(_lastMouseOverCell);
            }

            var fieldCell = _field.GetCell(cX, cY);
            fieldCell.Hovered  = true;
            _lastMouseOverCell = fieldCell;

            _fieldPresenter.UpdateCell(fieldCell);
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            var cY = _fieldPresenter.ConvertMouseYToCellY(y);

            var fieldCell = _field.GetCell(cX, cY);
            fieldCell.Clicked      = true;
            _lastMouseDownOverCell = fieldCell;

            _fieldPresenter.UpdateCell(fieldCell);
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            // this small check is needed for mouse release not over initially clicked cell (click and drag behavior)
            if (_lastMouseDownOverCell is not null)
            {
                _lastMouseDownOverCell.Clicked = false;
                _fieldPresenter.UpdateCell(_lastMouseDownOverCell);
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

            _fieldPresenter.UpdateCell(fieldCell);
        }
    }
}
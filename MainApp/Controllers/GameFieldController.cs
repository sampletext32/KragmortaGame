using System;
using System.Diagnostics;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class GameFieldController : ControllerBase
    {
        private FieldCell _lastMouseOverCell = null;
        private int _lastMouseOverCellIndex = -1;
        private FieldCell _lastMouseDownOverCell = null;
        private int _lastMouseDownOverCellIndex = -1;

        private readonly GameField _field;

        public GameFieldController(GameField field)
        {
            _field = field;
        }

        /// <summary>
        /// Returns the CellType of a cell by the passed coordinates x and y.
        /// <remarks>Doesn't make any checks for indices</remarks>
        /// </summary>
        public CellType GetCellType(int cX, int cY)
        {
            return _field.GetCell(cX, cY).Type;
        }

        public void OnMouseLeft()
        {
            if (_lastMouseOverCell is not null)
            {
                _lastMouseOverCell.Hovered = false;
                _lastMouseOverCell.MarkDirty();
                _lastMouseOverCell = null;
            }
        }

        public override void OnMouseMoved(int x, int y)
        {
            // int cX = _fieldPresenter.ConvertMouseXToCellX(x);
            // int cY = _fieldPresenter.ConvertMouseYToCellY(y);
            //
            // if (_lastMouseOverCell is not null)
            // {
            //     _lastMouseOverCell.Hovered = false;
            //     _fieldPresenter.UpdateCell(_lastMouseOverCell);
            // }
            //
            // var fieldCell = _field.GetCell(cX, cY);
            // fieldCell.Hovered  = true;
            // _lastMouseOverCell = fieldCell;
            //
            // _fieldPresenter.UpdateCell(fieldCell);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            // var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            // var cY = _fieldPresenter.ConvertMouseYToCellY(y);
            //
            // var fieldCell = _field.GetCell(cX, cY);
            // fieldCell.Clicked      = true;
            // _lastMouseDownOverCell = fieldCell;
            //
            // _fieldPresenter.UpdateCell(fieldCell);
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            // this small check is needed for mouse release not over initially clicked cell (click and drag behavior)
            if (_lastMouseDownOverCell is not null)
            {
                _lastMouseDownOverCell.Clicked = false;
                _lastMouseDownOverCell.MarkDirty();
                _lastMouseDownOverCell = null;
            }

            // if (!_fieldPresenter.IsMouseWithinBounds(x, y))
            // {
            //     return;
            // }
            //
            // var cX = _fieldPresenter.ConvertMouseXToCellX(x);
            // var cY = _fieldPresenter.ConvertMouseYToCellY(y);
            //
            // var fieldCell = _field.GetCell(cX, cY);
            // fieldCell.Clicked = false;
            //
            // _fieldPresenter.UpdateCell(fieldCell);
        }
    }
}
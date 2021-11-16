using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class GameFieldController
    {
        private FieldCell _lastMouseOverCell = null;
        private FieldCell _lastMouseDownOverCell = null;

        private FieldCell _moveTargetLeftCell = null;
        private FieldCell _moveTargetRightCell = null;
        private FieldCell _moveTargetTopCell = null;
        private FieldCell _moveTargetBottomCell = null;

        private readonly GameField _field;
        private readonly GameFieldPresenter _fieldPresenter;

        public GameFieldController(GameField field, GameFieldPresenter fieldPresenter)
        {
            _field          = field;
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
                _lastMouseDownOverCell = null;
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

        private bool TryClearMoveTarget(FieldCell cell)
        {
            if (cell is not null)
            {
                cell.IsPossibleMoveTarget = false;
                _fieldPresenter.UpdateCell(cell);
                return true;
            }

            return false;
        }

        private bool TryMakeMoveTarget(FieldCell cell, CellType cellTypeMask)
        {
            if (cell is null) return false;

            if ((cell.Type & cellTypeMask) == CellType.Empty) return true;

            cell.IsPossibleMoveTarget = true;
            _fieldPresenter.UpdateCell(cell);

            return true;
        }

        public void ClearMoveTargets()
        {
            if (TryClearMoveTarget(_moveTargetLeftCell)) _moveTargetLeftCell     = null;
            if (TryClearMoveTarget(_moveTargetRightCell)) _moveTargetRightCell   = null;
            if (TryClearMoveTarget(_moveTargetTopCell)) _moveTargetTopCell       = null;
            if (TryClearMoveTarget(_moveTargetBottomCell)) _moveTargetBottomCell = null;
        }

        public void HighlightMoveTargets(int heroX, int heroY, CellType cellTypeMask)
        {
            _moveTargetLeftCell   = heroX != 0 ? _field.GetCell(heroX - 1, heroY) : null;
            _moveTargetRightCell  = heroX != _field.SizeX - 1 ? _field.GetCell(heroX + 1, heroY) : null;
            _moveTargetTopCell    = heroY != 0 ? _field.GetCell(heroX, heroY - 1) : null;
            _moveTargetBottomCell = heroY != _field.SizeY - 1 ? _field.GetCell(heroX, heroY + 1) : null;

            TryMakeMoveTarget(_moveTargetLeftCell, cellTypeMask);
            TryMakeMoveTarget(_moveTargetRightCell, cellTypeMask);
            TryMakeMoveTarget(_moveTargetBottomCell, cellTypeMask);
            TryMakeMoveTarget(_moveTargetTopCell, cellTypeMask);
        }
    }
}
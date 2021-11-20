using System;
using System.Collections.Generic;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class PathController : ControllerBase
    {
        private GameField _field;
        private GameFieldPresenter _fieldPresenter;
        private ShiftController _shiftController;
        private List<FieldCell> _highlightedCells = new List<FieldCell>(4);

        public PathController(GameField field, GameFieldPresenter fieldPresenter,
            ShiftController shiftController)
        {
            _field           = field;
            _fieldPresenter  = fieldPresenter;
            _shiftController = shiftController;
        }

        public void HighlightPaths()
        {
            var hero = _shiftController.HeroModel;

            // TODO: Don't expose MovementDeck directly, but instead use MovementDeckController and get the active card
            var usingMovementCard = hero.MovementDeck.GetUsingMovementCard();

            if (usingMovementCard is null) return;

            // Get possible types to move.
            CellType possibleTypes = GetPossibleTypes(usingMovementCard);

            // Check neighboring for the hero cells.

            if (hero.FieldX != _field.SizeX - 1)
            {
                var cell = _field.GetCell(hero.FieldX + 1, hero.FieldY);
                HighlightCellIfItHasAnyType(cell, possibleTypes);
            }

            if (hero.FieldX != 0)
            {
                var cell = _field.GetCell(hero.FieldX - 1, hero.FieldY);
                HighlightCellIfItHasAnyType(cell, possibleTypes);
            }

            if (hero.FieldY != 0)
            {
                var cell = _field.GetCell(hero.FieldX, hero.FieldY - 1);
                HighlightCellIfItHasAnyType(cell, possibleTypes);
            }

            if (hero.FieldY != _field.SizeY - 1)
            {
                var cell = _field.GetCell(hero.FieldX, hero.FieldY + 1);
                HighlightCellIfItHasAnyType(cell, possibleTypes);
            }
        }

        private void HighlightCellIfItHasAnyType(FieldCell cell, CellType possibleTypes)
        {
            // If a cell is neighbor and has the correct symbol, highlight it.
            if ((cell.Type & possibleTypes) != CellType.Empty)
            {
                cell.Highlighted = true;
                _highlightedCells.Add(cell);
                _fieldPresenter.UpdateCell(cell);
            }
        }

        private static CellType GetPossibleTypes(MovementCard card)
        {
            CellType type =
                (card.HasUsedFirstType ? CellType.Empty : card.FirstType) |
                (card.HasUsedSecondType ? CellType.Empty : card.SecondType);

            return type;
        }

        public void UnhighlightPaths()
        {
            foreach (var highlightedCell in _highlightedCells)
            {
                highlightedCell.Highlighted = false;
                _fieldPresenter.UpdateCell(highlightedCell);
            }

            _highlightedCells.Clear();
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void OnMouseMoved(int x, int y)
        {
        }
    }
}
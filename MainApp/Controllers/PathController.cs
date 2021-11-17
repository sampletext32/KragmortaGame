using System;
using System.Collections.Generic;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Presenters;

namespace MainApp.Controllers
{
    public class PathController
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
            var hero              = _shiftController.CurrentHero;
            var usingMovementCard = hero.MovementDeck.GetUsingMovementCard();

            if (usingMovementCard is not null)
            {
                // Get possible symbols to move.
                List<CellType> possibleSymbolsToMove = getPossibleSymbolsToMove(usingMovementCard);
                // Get hero's coordinates.
                // Check neighboring for the hero cells.
                for (var i = 0; i < 4; ++i)
                {
                    var       xChecked = i < 2;
                    FieldCell currCell;
                    try
                    {
                        if (!xChecked)
                        {
                            currCell = _field.GetCell(hero.FieldX + (int)Math.Pow(-1, i % 2), hero.FieldY);
                        }
                        else
                        {
                            currCell = _field.GetCell(hero.FieldX, hero.FieldY + (int)Math.Pow(-1, i % 2));
                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        continue;
                    }

                    // If a cell is neighbor and has the correct symbol, highlight it.
                    if (possibleSymbolsToMove.Contains(currCell.Type))
                    {
                        _fieldPresenter.HighlightCell(currCell);
                        _highlightedCells.Add(currCell);
                    }
                }
            }
        }

        private List<CellType> getPossibleSymbolsToMove(MovementCard card)
        {
            List<CellType> result = new List<CellType>(2);

            if (!card.HasUsedFirstType)
            {
                result.Add(card.FirstType);
            }

            if (!card.HasUsedSecondType)
            {
                result.Add(card.SecondType);
            }

            return result;
        }

        public void UnhighlightPaths()
        {
            foreach (var highlightedCell in _highlightedCells)
            {
                _fieldPresenter.UnhighlightCell(highlightedCell);
            }

            _highlightedCells.Clear();
        }
    }
}
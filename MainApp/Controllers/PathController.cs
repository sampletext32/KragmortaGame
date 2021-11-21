using System.Collections.Generic;
using System.Linq;
using MainApp.Entities;
using MainApp.Enums;

namespace MainApp.Controllers
{
    public class PathController : ControllerBase
    {
        private List<PathCell> _pathCells = new List<PathCell>(4);

        public PathController(List<PathCell> pathCells)
        {
        }

        public void HighlightPaths()
        {
            // var hero = _shiftController.HeroModel;
            //
            // // TODO: Don't expose MovementDeck directly, but instead use MovementDeckController and get the active card
            // var usingMovementCard = hero.MovementDeck.GetUsingMovementCard();
            //
            // if (usingMovementCard is null) return;
            //
            // // Get possible types to move.
            // CellType possibleTypes = GetPossibleTypes(usingMovementCard);
            //
            // // Check neighboring for the hero cells.
            //
            // if (hero.FieldX != _field.SizeX - 1)
            // {
            //     var cell = _field.GetCell(hero.FieldX + 1, hero.FieldY);
            //     HighlightCellIfItHasAnyType(cell, possibleTypes);
            // }
            //
            // if (hero.FieldX != 0)
            // {
            //     var cell = _field.GetCell(hero.FieldX - 1, hero.FieldY);
            //     HighlightCellIfItHasAnyType(cell, possibleTypes);
            // }
            //
            // if (hero.FieldY != 0)
            // {
            //     var cell = _field.GetCell(hero.FieldX, hero.FieldY - 1);
            //     HighlightCellIfItHasAnyType(cell, possibleTypes);
            // }
            //
            // if (hero.FieldY != _field.SizeY - 1)
            // {
            //     var cell = _field.GetCell(hero.FieldX, hero.FieldY + 1);
            //     HighlightCellIfItHasAnyType(cell, possibleTypes);
            // }
        }

        private void HighlightCellIfItHasAnyType(FieldCell cell, CellType possibleTypes)
        {
            // If a cell is neighbor and has the correct symbol, highlight it.
            // if ((cell.Type & possibleTypes) != CellType.Empty)
            // {
            //     cell.Highlighted = true;
            //     _highlightedCells.Add(cell);
            //     _fieldPresenter.UpdateCell(cell);
            // }
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
            // foreach (var highlightedCell in _highlightedCells)
            // {
            //     highlightedCell.Highlighted = false;
            //     _fieldPresenter.UpdateCell(highlightedCell);
            // }
            //
            // _highlightedCells.Clear();
        }

        public bool TryGetCell(int selectedCellX, int selectedCellY, out PathCell pathCell)
        {
            foreach (var cell in _pathCells)
            {
                if (cell.X == selectedCellX && cell.Y == selectedCellY)
                {
                    pathCell = cell;
                    return true;
                }
            }

            pathCell = null;
            return false;
        }

        public CellType GetCellType(int selectedCellX, int selectedCellY)
        {
            return _pathCells.First(c => c.X == selectedCellX && c.Y == selectedCellY).Type;
        }

        public void ComputePath(List<AbstractCell> rawPaths, MovementCard card)
        {
            for (int i = 0; i < rawPaths.Count; i++)
            {
                // if (!(!card.HasUsedFirstType && card.FirstType == rawPaths[i].Type)) continue;
                // if (!(!card.HasUsedSecondType && card.SecondType == rawPaths[i].Type)) continue;

                // if (card.HasUsedFirstType || card.FirstType != rawPaths[i].Type) continue;
                // if (card.HasUsedSecondType || card.SecondType != rawPaths[i].Type) continue;

                if (!card.HasUsedFirstType && card.FirstType == rawPaths[i].Type)
                {
                    _pathCells.Add(new PathCell()
                    {
                        X     = rawPaths[i].X,
                        Y     = rawPaths[i].Y,
                        Type  = rawPaths[i].Type,
                        Dirty = true
                    });
                }
                else if (!card.HasUsedSecondType && card.SecondType == rawPaths[i].Type)
                {
                    _pathCells.Add(new PathCell()
                    {
                        X     = rawPaths[i].X,
                        Y     = rawPaths[i].Y,
                        Type  = rawPaths[i].Type,
                        Dirty = true
                    });
                }
            }
        }
    }
}
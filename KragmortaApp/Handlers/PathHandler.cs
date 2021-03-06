using System;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Cells;

namespace KragmortaApp.Handlers
{
    public class PathHandler : AbstractHandler
    {
        private readonly PathController _pathController;
        private readonly PushController _pushController;
        private readonly GameFieldController _gameFieldController;
        private readonly MovementDecksController _movementDecksController;
        private readonly ShiftController _shiftController;
        private readonly PortalController _portalController;
        private readonly FinishButtonController _finishButtonController;
        private readonly RigorController _rigorController;
        private readonly ProfilesController _profilesController;

        public PathHandler(
            PathController pathController,
            PushController pushController,
            GameFieldController gameFieldController,
            MovementDecksController movementDecksController,
            ShiftController shiftController,
            PortalController portalController,
            FinishButtonController finishButtonController,
            ProfilesController profilesController,
            RigorController rigorController)
        {
            _pathController          = pathController;
            _pushController          = pushController;
            _gameFieldController     = gameFieldController;
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
            _portalController        = portalController;
            _finishButtonController  = finishButtonController;
            _profilesController      = profilesController;
            _rigorController         = rigorController;
        }

        public override void RawOnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
        }

        public void OnPathCellClicked(int pathCellX, int pathCellY, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            // NOTE: no need to check the result of "Try", because a selected cell here is known to be the path cell
            _pathController.TryGetCell(pathCellX, pathCellY, out var pathCell);

            // Here we have 3 phases. 
            // 1 - no card is selected
            // 2 - a card is selected
            // 3 - a card is activated

            if (_movementDecksController.HasSelectedCard())
            {
                // case 2
                ProcessCardIsSelected(pathCellX, pathCellY, pathCell);
            }
            else if (_movementDecksController.HasActivatedCard())
            {
                // case 3 
                ProcessCardIsActivated(pathCellX, pathCellY, pathCell);
            }
            else
            {
                //case 1
                throw new KragException("Unreachable");
            }
        }

        private void ProcessCardIsActivated(int pathCellX, int pathCellY, PathCell pathCell)
        {
            _movementDecksController.SpendType(pathCell.Type);

            int   heroPreviousX, heroPreviousY;
            IHero movingHero;
            if (_movementDecksController.ActivatedMovementCard.MovementCardType == MovementCardType.Goblin)
            {
                heroPreviousX = _shiftController.Hero.FieldX;
                heroPreviousY = _shiftController.Hero.FieldY;

                movingHero = _shiftController.Hero;
                movingHero.SetFieldPosition(pathCellX, pathCellY);
            }
            else
            {
                heroPreviousX = _rigorController.Model.FieldX;
                heroPreviousY = _rigorController.Model.FieldY;

                movingHero = _rigorController.Model;
                movingHero.SetFieldPosition(pathCellX, pathCellY);
            }

            if (Engine.Instance.Settings.EnableSounds)
            {
                Engine.Instance.SoundCache.GetOrCache("whoosh_move").Play();
            }

            _movementDecksController.DismissActivatedCard();
            _movementDecksController.PullNewCard();

            // If portal is selected.
            if (_gameFieldController.GetCell(pathCellX, pathCellY).IsPortal)
            {
                _portalController.SetAllVisibleExcept(pathCellX, pathCellY);
                _shiftController.WhoEnteredPortal = movingHero;
                _pathController.ClearPaths();
                _finishButtonController.HideButton();
                return;
            }

            // If workbench is selected
            if (_gameFieldController.GetCell(pathCellX, pathCellY).IsWorkbench)
            {
                _profilesController.CurrentController.GiveBook();

                var teleportingCell = _gameFieldController.GetSpawnCell();
                _shiftController.Hero.SetFieldPosition(teleportingCell.X, teleportingCell.Y);

                HeroModel sameCellHero;
                if ((sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                        h != _shiftController.Hero && h.FieldX == _shiftController.Hero.FieldX &&
                        h.FieldY == _shiftController.Hero.FieldY)) is not null)
                {
                    ProcessCellOverflow(_shiftController.Hero.FieldX, _shiftController.Hero.FieldY,
                        pathCellX, pathCellY, sameCellHero);
                }

                _pathController.ClearPaths();

                _shiftController.ActivateNextPlayer();
                _profilesController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();

                return;
            }

            // In the destination cell there are Rigor and Goblin
            if (CheckRigorAndGoblinOnSameCell(out var victimHero))
            {
                var teleportingCell = _gameFieldController.GetSpawnCell();
                victimHero.SetFieldPosition(teleportingCell.X, teleportingCell.Y);
                _profilesController.DealDamageToHero(victimHero);

                HeroModel sameCellHero;
                if ((sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                        h != victimHero && h.FieldX == victimHero.FieldX && h.FieldY == victimHero.FieldY)) is not null)
                {
                    ProcessCellOverflow(victimHero.FieldX, victimHero.FieldY,
                        _rigorController.Model.FieldX, _rigorController.Model.FieldY, sameCellHero);
                }
            }
            // In the destination cell there are 2 heroes
            else if (CheckCellOverflow(pathCellX, pathCellY, out var sameCellHero))
            {
                ProcessCellOverflow(pathCellX, pathCellY, heroPreviousX, heroPreviousY, sameCellHero);
                // No return move to pusher in this case (second move)

                return;
            }


            // clear visible path
            _pathController.ClearPaths();

            _shiftController.ActivateNextPlayer();
            _profilesController.ActivateNextPlayer();
            _movementDecksController.ActivateNextDeck();
        }

        private void ProcessCardIsSelected(int pathCellX, int pathCellY, PathCell pathCell)
        {
            _movementDecksController.ActivateSelectedCard();

            _movementDecksController.SpendType(pathCell.Type);

            int   heroPreviousX, heroPreviousY;
            IHero movingHero;
            if (_movementDecksController.ActivatedMovementCard.MovementCardType == MovementCardType.Goblin)
            {
                heroPreviousX = _shiftController.Hero.FieldX;
                heroPreviousY = _shiftController.Hero.FieldY;

                movingHero = _shiftController.Hero;
                movingHero.SetFieldPosition(pathCellX, pathCellY);
            }
            else
            {
                heroPreviousX = _rigorController.Model.FieldX;
                heroPreviousY = _rigorController.Model.FieldY;

                movingHero = _rigorController.Model;
                movingHero.SetFieldPosition(pathCellX, pathCellY);
            }


            if (Engine.Instance.Settings.EnableSounds)
            {
                Engine.Instance.SoundCache.GetOrCache("whoosh_move").Play();
            }


            // If portal is selected
            if (_gameFieldController.GetCell(pathCellX, pathCellY).IsPortal)
            {
                _finishButtonController.HideButton();
                _movementDecksController.DismissActivatedCard();
                _movementDecksController.PullNewCard();
                _portalController.SetAllVisibleExcept(pathCellX, pathCellY);
                _shiftController.WhoEnteredPortal = movingHero;
                _pathController.ClearPaths();
                return;
            }

            // If workbench is selected
            if (_gameFieldController.GetCell(pathCellX, pathCellY).IsWorkbench)
            {
                _movementDecksController.DismissActivatedCard();
                _movementDecksController.PullNewCard();

                _profilesController.CurrentController.GiveBook();

                var teleportingCell = _gameFieldController.GetSpawnCell();
                _shiftController.Hero.SetFieldPosition(teleportingCell.X, teleportingCell.Y);

                HeroModel sameCellHero;
                if ((sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                        h != _shiftController.Hero && h.FieldX == _shiftController.Hero.FieldX &&
                        h.FieldY == _shiftController.Hero.FieldY)) is not null)
                {
                    ProcessCellOverflow(_shiftController.Hero.FieldX, _shiftController.Hero.FieldY,
                        pathCellX, pathCellY, sameCellHero);
                }

                _pathController.ClearPaths();

                _shiftController.ActivateNextPlayer();
                _profilesController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();

                return;
            }

            // In the destination cell there are Rigor and Goblin
            if (CheckRigorAndGoblinOnSameCell(out var victimHero))
            {
                var teleportingCell = _gameFieldController.GetSpawnCell();
                victimHero.SetFieldPosition(teleportingCell.X, teleportingCell.Y);
                _profilesController.DealDamageToHero(victimHero);
                _pathController.ClearPaths();

                _movementDecksController.DismissActivatedCard();
                _movementDecksController.PullNewCard();

                _shiftController.ActivateNextPlayer();
                _profilesController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();

                HeroModel sameCellHero;
                if ((sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                        h != victimHero && h.FieldX == victimHero.FieldX && h.FieldY == victimHero.FieldY)) is not null)
                {
                    ProcessCellOverflow(victimHero.FieldX, victimHero.FieldY,
                        _rigorController.Model.FieldX, _rigorController.Model.FieldY, sameCellHero);
                }

                return;
            }
            // In the destination cell there are 2 heroes
            else if (CheckCellOverflow(pathCellX, pathCellY, out var sameCellHero))
            {
                ProcessCellOverflow(pathCellX, pathCellY, heroPreviousX, heroPreviousY, sameCellHero);
                _pushController.SetReturnMoveToPusher(_shiftController.Hero);

                return;
            }


            // regenerate visible path
            _pathController.RawPath.Clear();
            _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _pathController.RawPath);

            if (!_pathController.TrySetVisiblePath(_movementDecksController.ActivatedMovementCard))
            {
                Console.WriteLine("No moves with selected card");
                _movementDecksController.DismissActivatedCard();
                _movementDecksController.PullNewCard();
                _shiftController.ActivateNextPlayer();
                _profilesController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();
            }
        }

        private bool CheckRigorAndGoblinOnSameCell(out HeroModel victim)
        {
            return (victim = GameState.Instance.Heroes.FirstOrDefault(h =>
                h.FieldX == _rigorController.Model.FieldX && h.FieldY == _rigorController.Model.FieldY)) is not null;
        }

        private void ProcessCellOverflow(int pathCellX, int pathCellY, int heroPreviousX, int heroPreviousY,
            HeroModel sameCellHero)
        {
            _finishButtonController.HideButton();

            _pathController.ClearPaths();

            // Highlight paths of push

            _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _pushController.RawPush);

            _pushController.Except(heroPreviousX, heroPreviousY);
            _pushController.TrySetVisiblePush();

            _pushController.SetVictim(sameCellHero);
        }

        private bool CheckCellOverflow(int pathCellX, int pathCellY, out HeroModel sameCellHero)
        {
            return (sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                h != _shiftController.Hero && h.FieldX == pathCellX && h.FieldY == pathCellY)) is not null;
        }
    }
}
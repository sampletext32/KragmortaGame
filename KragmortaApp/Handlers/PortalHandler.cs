using System;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;

namespace KragmortaApp.Handlers
{
    public class PortalHandler : AbstractHandler
    {
        private readonly PortalController _portalController;
        private readonly ShiftController _shiftController;
        private readonly MovementDecksController _movementDecksController;
        private readonly GameFieldController _gameFieldController;
        private readonly PushController _pushController;
        private readonly FinishButtonController _finishButtonController;
        private readonly ProfilesController _profilesController;
        private readonly RigorController _rigorController;

        public PortalHandler(
            PortalController portalController,
            ShiftController shiftController,
            MovementDecksController movementDecksController,
            GameFieldController gameFieldController,
            PushController pushController,
            FinishButtonController finishButtonController,
            ProfilesController profilesController, RigorController rigorController)
        {
            _portalController        = portalController;
            _shiftController         = shiftController;
            _movementDecksController = movementDecksController;
            _gameFieldController     = gameFieldController;
            _pushController          = pushController;
            _finishButtonController  = finishButtonController;
            _profilesController      = profilesController;
            _rigorController    = rigorController;
        }

        public void OnPortalCellClicked(int pathCellX, int pathCellY, KragMouseButton mouseButton)
        {
            // Step on the portal
            //      If there is another player, push him out of this cell
            //      
            // Highlight other portals except the one where the player stands right now.
            // (*) Player clicks on any highlighted portal.
            // He teleports. (Set his coords to the coords of the portal)
            //      If the cell, where the current player is teleporting, is reserved by another player, current
            //      player pushes that player. Therefore, the chain of pushing starts.
            //          In case during the chain of pushing a player steps on a portal, look at the very first
            //          point again.


            // This method activates from (*) point of the plan above.


            var heroPreviousX = _shiftController.Hero.FieldX;
            var heroPreviousY = _shiftController.Hero.FieldY;

            _shiftController.WhoEnteredPortal.SetFieldPosition(pathCellX, pathCellY);

            _portalController.SetInvisiblePortals();
            
            
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
                ProcessCellOverflow(victimHero.FieldX, victimHero.FieldY,
                    _rigorController.Model.FieldX, _rigorController.Model.FieldY, sameCellHero);
            }

            _shiftController.ActivateNextPlayer();
            _profilesController.ActivateNextPlayer();
            _movementDecksController.ActivateNextDeck();
            _finishButtonController.ShowButton();
        }
        
        private bool CheckCellOverflow(int pathCellX, int pathCellY, out HeroModel sameCellHero)
        {
            return (sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                h != _shiftController.Hero && h.FieldX == pathCellX && h.FieldY == pathCellY)) is not null;
        }

        private bool CheckRigorAndGoblinOnSameCell(out HeroModel victimHero)
        {
            return (victimHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                h.FieldX == _rigorController.Model.FieldX && h.FieldY == _rigorController.Model.FieldY)) is not null;
        }
        
        private void ProcessCellOverflow(int pathCellX, int pathCellY, int heroPreviousX, int heroPreviousY,
            HeroModel sameCellHero)
        {
            // Highlight paths of push
            _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _pushController.RawPush);

            _pushController.Except(heroPreviousX, heroPreviousY);
            _pushController.TrySetVisiblePush();

            _pushController.SetVictim(sameCellHero);
            // No return move to pusher in this case (second move)
        }
    }
}
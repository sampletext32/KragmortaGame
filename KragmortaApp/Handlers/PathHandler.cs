using System;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;

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
            _profilesController = profilesController;
            _rigorController    = rigorController;
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
                _movementDecksController.ActivateSelectedCard();

                _movementDecksController.SpendType(pathCell.Type);

                // var heroPreviousX = _shiftController.Hero.FieldX;
                // var heroPreviousY = _shiftController.Hero.FieldY;
                int heroPreviousX, heroPreviousY;
                if (_movementDecksController.LastSelectedMovementCard.MovementCardType == MovementCardType.Goblin)
                {
                    heroPreviousX = _shiftController.Hero.FieldX;
                    heroPreviousY = _shiftController.Hero.FieldY;
                    
                    _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);
                }
                else
                {
                    heroPreviousX = _rigorController.Model.FieldX;
                    heroPreviousY = _rigorController.Model.FieldY;
                    
                    _rigorController.Model.SetFieldPosition(pathCellX, pathCellY);
                }

                
                if (Engine.Instance.Settings.EnableSounds)
                {
                    Engine.Instance.SoundCache.GetOrCache("whoosh_move").Play();
                }


                if (_gameFieldController.GetCell(pathCellX, pathCellY).IsPortal)
                {
                    _finishButtonController.HideButton();
                    _movementDecksController.DismissActivatedCard();
                    _movementDecksController.PullNewCard();
                    _portalController.SetAllVisibleExcept(pathCellX, pathCellY);
                    _pathController.ClearPaths();
                    return;
                }

                // In the destination cell there are 2 heroes
                HeroModel sameCellHero;
                if ((sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                        h != _shiftController.Hero && h.FieldX == pathCellX && h.FieldY == pathCellY)) is not null)
                {
                    _finishButtonController.HideButton();
                    // use sameCellHero for further processing
                    // Console.WriteLine($"Hero {_shiftController.Hero.Profile.Nickname} pushes {sameCellHero.Profile.Nickname}");

                    _pathController.ClearPaths();

                    // Highlight paths of push

                    _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _pushController.RawPush);

                    _pushController.Except(heroPreviousX, heroPreviousY);
                    _pushController.TrySetVisiblePush();

                    _pushController.SetVictim(sameCellHero);
                    _pushController.SetReturnMoveToPusher(_shiftController.Hero);

                    return;
                }

                // In the destination cell there are Rigor and Goblin
                if (_shiftController.Hero.FieldX == _rigorController.Model.FieldX &&
                    _shiftController.Hero.FieldY == _rigorController.Model.FieldY)
                {
                    _profilesController.CurrentController.TakeDamage();
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
            else if (_movementDecksController.HasActivatedCard())
            {
                // case 3 
                _movementDecksController.SpendType(pathCell.Type);

                var heroPreviousX = _shiftController.Hero.FieldX;
                var heroPreviousY = _shiftController.Hero.FieldY;

                _shiftController.Hero.SetFieldPosition(pathCellX, pathCellY);
                if (Engine.Instance.Settings.EnableSounds)
                {
                    Engine.Instance.SoundCache.GetOrCache("whoosh_move").Play();
                }

                _movementDecksController.DismissActivatedCard();
                _movementDecksController.PullNewCard();

                if (_gameFieldController.GetCell(pathCellX, pathCellY).IsPortal)
                {
                    _portalController.SetAllVisibleExcept(pathCellX, pathCellY);
                    _pathController.ClearPaths();
                    _finishButtonController.HideButton();
                    return;
                }

                // In the destination cell there are 2 heroes
                HeroModel sameCellHero;
                if ((sameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                        h != _shiftController.Hero && h.FieldX == pathCellX && h.FieldY == pathCellY)) is not null)
                {
                    _finishButtonController.HideButton();

                    // use sameCellHero for further processing
                    // Console.WriteLine($"Hero {sameCellHero.Profile.Nickname} is being pushed by {_shiftController.Hero.Profile.Nickname}");

                    _pathController.ClearPaths();

                    // Highlight paths of push

                    _gameFieldController.CollectNeighboringCells(pathCellX, pathCellY, _pushController.RawPush);

                    _pushController.Except(heroPreviousX, heroPreviousY);
                    _pushController.TrySetVisiblePush();

                    _pushController.SetVictim(sameCellHero);
                    // No return move to pusher in this case (second move)

                    return;
                }

                // clear visible path
                _pathController.ClearPaths();

                _shiftController.ActivateNextPlayer();
                _profilesController.ActivateNextPlayer();
                _movementDecksController.ActivateNextDeck();
            }
            else
            {
                //case 1
                throw new KragException("Unreachable");
            }
        }
    }
}
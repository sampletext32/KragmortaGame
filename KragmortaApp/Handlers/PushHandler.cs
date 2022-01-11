﻿using System;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;

namespace KragmortaApp.Handlers
{
    public class PushHandler : AbstractHandler
    {
        private readonly PushController _pushController;
        private readonly PathController _pathController;
        private readonly GameFieldController _gameFieldController;
        private readonly MovementDecksController _movementDecksController;
        private readonly ShiftController _shiftController;
        private readonly PortalController _portalController;
        private readonly ProfilesController _profilesController;
        private readonly FinishButtonController _finishButtonController;

        public PushHandler(
            PushController pushController,
            PathController pathController,
            GameFieldController gameFieldController,
            MovementDecksController movementDecksController,
            ShiftController shiftController,
            FinishButtonController finishButtonController,
            PortalController portalController,
            ProfilesController profilesController
        )
        {
            _pushController          = pushController;
            _pathController          = pathController;
            _gameFieldController     = gameFieldController;
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
            _finishButtonController  = finishButtonController;
            _portalController        = portalController;
            _profilesController = profilesController;
        }

        public override void RawOnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
        }

        public void OnPushCellClicked(int nextCellX, int nextCellY, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            _pushController.TryGetCell(nextCellX, nextCellY, out var nextCell);

            var victimPreviousX = _pushController.PushedStateModel.Victim.FieldX;
            var victimPreviousY = _pushController.PushedStateModel.Victim.FieldY;

            var isPortal = _gameFieldController.GetCell(nextCellX, nextCellY).IsPortal;
            if (isPortal)
            {
                var randPortalCell = _portalController.RandomExcept(nextCellX, nextCellY);
                nextCellX = randPortalCell.X;
                nextCellY = randPortalCell.Y;
                _pushController.PushedStateModel.Victim.SetFieldPosition(nextCellX, nextCellY);
            }
            else
            {
                // push victim to position
                _pushController.PushedStateModel.Victim.SetFieldPosition(nextCell.X, nextCell.Y);

                // Console.WriteLine($"{_pushController.Victim.Profile.Nickname} was pushed to ({nextCell.X},{nextCell.Y})");
            }

            if (Engine.Instance.Settings.EnableSounds)
            {
                Engine.Instance.SoundCache.GetOrCache("whoosh_push").Play();
            }

            _pushController.ClearPush();

            // From this moment we have 2 situations
            // 1 - victim is now in another hero position
            // 2 - victim is now in empty cell

            // In the destination cell there are 2 heroes
            HeroModel victimSameCellHero;
            if ((victimSameCellHero = GameState.Instance.Heroes.FirstOrDefault(h =>
                    h != _pushController.PushedStateModel.Victim && h.FieldX == nextCellX && h.FieldY == nextCellY)) is not null)
            {
                // Case 1

                // use sameCellHero for further processing
                // Console.WriteLine($"Hero {_pushController.Victim.Profile.Nickname} pushes {victimSameCellHero.Profile.Nickname}");

                // Highlight new paths of push

                _gameFieldController.CollectNeighboringCells(nextCellX, nextCellY, _pushController.RawPush);

                _pushController.Except(victimPreviousX, victimPreviousY);
                _pushController.TrySetVisiblePush();

                _pushController.ClearVictim();
                _pushController.SetVictim(victimSameCellHero);

                // Don't clear pusher, because at the end of a push chain we MUST return the turn back to original pusher
            }
            else
            {
                // Case 2

                if (_pushController.PushedStateModel.ShouldReturnMoveToPusher)
                {
                    var pusher = _pushController.PushedStateModel.Pusher;
                    _pathController.ClearPaths();
                    _gameFieldController.CollectNeighboringCells(pusher.FieldX, pusher.FieldY, _pathController.RawPath);

                    // By this moment pusher MUST have an activated card
                    _pathController.TrySetVisiblePath(_movementDecksController.ActivatedMovementCard);
                    _pushController.ClearVictim();
                    _pushController.ClearReturnMoveToPusher();
                }
                else
                {
                    // Activate next player for further turns
                    _shiftController.ActivateNextPlayer();
                    _profilesController.ActivateNextPlayer();
                    _movementDecksController.ActivateNextDeck();
                }

                _finishButtonController.ShowButton();
            }
        }
    }
}
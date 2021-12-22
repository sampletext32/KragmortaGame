using System;
using System.Linq;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;

namespace KragmortaApp.Handlers
{
    public class PushHandler : AbstractHandler
    {
        private readonly PushController _pushController;
        private readonly PathController _pathController;
        private GameFieldController _gameFieldController;
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;

        public PushHandler(
            PushController pushController,
            PathController pathController,
            GameFieldController gameFieldController,
            MovementDecksController movementDecksController,
            ShiftController shiftController
        )
        {
            _pushController          = pushController;
            _pathController          = pathController;
            _gameFieldController     = gameFieldController;
            _movementDecksController = movementDecksController;
            _shiftController         = shiftController;
        }

        public override void RawOnMousePressed(int selectedCellX, int selectedCellY, KragMouseButton mouseButton)
        {
        }

        public void OnPushCellClicked(int pushCellX, int pushCellY, KragMouseButton mouseButton)
        {
            if (mouseButton != KragMouseButton.Left) return;

            _pushController.TryGetCell(pushCellX, pushCellY, out var pushCell);

            var victimPreviousX = _pushController.Victim.FieldX;
            var victimPreviousY = _pushController.Victim.FieldY;

            // push victim to position
            _pushController.Victim.SetFieldPosition(pushCell.X, pushCell.Y);
            if (Engine.Instance.Settings.EnableSounds)
            {
                Engine.Instance.SoundCache.GetOrCache("whoosh_push").Play();
            }

            _pushController.ClearPush();

            Console.WriteLine($"{_pushController.Victim.Nickname} was pushed to ({pushCell.X},{pushCell.Y})");

            // From this moment we have 2 situations
            // 1 - victim is now in another hero position
            // 2 - victim is now in empty cell

            // In the destination cell there are 2 heroes
            HeroModel victimSameCellHero;
            if ((victimSameCellHero = GameState.Instance.Heroes.FirstOrDefault(h => h != _pushController.Victim && h.FieldX == pushCell.X && h.FieldY == pushCell.Y)) is not null)
            {
                // Case 1

                // use sameCellHero for further processing
                Console.WriteLine($"Hero {_pushController.Victim.Nickname} pushes {victimSameCellHero.Nickname}");

                // Highlight new paths of push

                _gameFieldController.CollectNeighboringCells(pushCell.X, pushCell.Y, _pushController.RawPush);

                _pushController.Except(victimPreviousX, victimPreviousY);
                _pushController.TrySetVisiblePush();

                _pushController.ClearVictim();
                _pushController.SetVictim(victimSameCellHero);

                // Don't clear pusher, because at the end of a push chain we MUST return the turn back to original pusher
            }
            else
            {
                // Case 2

                if (_pushController.ShouldReturnMoveToPusher)
                {
                    var pusher = _pushController.Pusher;
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
                    _movementDecksController.ActivateNextDeck();
                }
            }
        }
    }
}
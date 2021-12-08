using KragmortaApp.Controllers;
using KragmortaApp.Controllers.ContextMenus;

namespace KragmortaApp.Handlers.ContextMenus
{
    public class MovementCardContextMenuHandler : AbstractHandler
    {
        private MovementCardContextMenuController _movementCardContextMenuController;
        private MovementDecksController _movementDecksController;
        private ShiftController _shiftController;
        private PathController _pathController;

        public MovementCardContextMenuHandler(MovementCardContextMenuController movementCardContextMenuController, MovementDecksController movementDecksController, ShiftController shiftController, PathController pathController)
        {
            _movementCardContextMenuController = movementCardContextMenuController;
            _movementDecksController           = movementDecksController;
            _shiftController                   = shiftController;
            _pathController               = pathController;
        }

        public void Dismiss()
        {
            _movementCardContextMenuController.Dismiss();
        }

        public void DeleteCard()
        {
            var movementCard = _movementCardContextMenuController.MovementCardContextMenuModel.Card;

            _movementDecksController.DismissCard(movementCard);

            _pathController.ClearPaths();
            
            _movementDecksController.PullNewCard();
            
            _movementCardContextMenuController.Dismiss();
            
            _shiftController.NoticeCardDeletion();
        }
    }
}
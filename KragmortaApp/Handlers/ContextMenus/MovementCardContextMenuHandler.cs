using KragmortaApp.Controllers.ContextMenus;

namespace KragmortaApp.Handlers.ContextMenus
{
    public class MovementCardContextMenuHandler : AbstractHandler
    {
        private MovementCardContextMenuController _movementCardContextMenuController;

        public MovementCardContextMenuHandler(MovementCardContextMenuController movementCardContextMenuController)
        {
            _movementCardContextMenuController = movementCardContextMenuController;
        }

        public void Dismiss()
        {
            _movementCardContextMenuController.Dismiss();
        }
    }
}
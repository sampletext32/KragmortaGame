﻿using KragmortaApp.Entities;
using KragmortaApp.Entities.ContextMenus;

namespace KragmortaApp.Controllers.ContextMenus
{
    public class MovementCardContextMenuController : ControllerBase
    {
        private MovementCardContextMenuModel _movementCardContextMenuModel;

        public MovementCardContextMenuController(MovementCardContextMenuModel movementCardContextMenuModel)
        {
            _movementCardContextMenuModel = movementCardContextMenuModel;
        }

        public void SetCard(MovementCard card)
        {
            _movementCardContextMenuModel.Card    = card;
            _movementCardContextMenuModel.Visible = true;
            _movementCardContextMenuModel.MarkDirty();
        }

        public void Dismiss()
        {
            _movementCardContextMenuModel.Card    = null;
            _movementCardContextMenuModel.Visible = false;
            _movementCardContextMenuModel.MarkDirty();
        }

        public void SetPosition(int x, int y)
        {
            _movementCardContextMenuModel.X = x;
            _movementCardContextMenuModel.Y = y;
        }
    }
}
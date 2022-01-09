using System;
using KragmortaApp.Controllers;
using KragmortaApp.Entities;
using KragmortaApp.Layers;

namespace KragmortaApp.Handlers
{
    public class RigorHandler : AbstractHandler
    {
        private readonly RigorController _rigorController;

        public RigorHandler(RigorController rigorController)
        {
            _rigorController = rigorController;
        }
        
        public override void RawOnMouseMoved(int x, int y)
        {
        }

        public override void RawOnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void RawOnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void RawOnMouseLeft()
        {
        }
    }
}
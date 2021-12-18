using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class PortalController : ControllerBase
    {
        private Portal _portal;

        public PortalController(Portal portal)
        {
            _portal = portal;
        }
    }
}
using KragmortaApp.Entities;

namespace KragmortaApp.Controllers
{
    public class RigorController : ControllerBase
    {
        public RigorModel Model => _rigor;
        private RigorModel _rigor;

        public RigorController(RigorModel rigor)
        {
            _rigor = rigor;
        }
    }
}
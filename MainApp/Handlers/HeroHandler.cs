using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class HeroHandler : AbstractHandler
    {
        public HeroHandler(HeroController controller) : base(controller)
        {
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
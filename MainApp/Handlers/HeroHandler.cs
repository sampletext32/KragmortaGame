using MainApp.Controllers;

namespace MainApp.Handlers
{
    public class HeroHandler : AbstractHandler
    {
        public HeroHandler(HeroController controller) : base(controller)
        {
        }

        public override void OnMouseMoved(int x, int y)
        {
        }

        public override void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
        }

        public override void OnMouseLeft()
        {
        }
    }
}
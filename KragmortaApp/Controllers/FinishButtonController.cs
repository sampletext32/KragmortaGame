using KragmortaApp.Entities.Buttons;

namespace KragmortaApp.Controllers
{

    public class FinishButtonController : ControllerBase
    {
        private readonly FinishButtonModel _finishButtonModel;

        public FinishButtonController(FinishButtonModel finishButtonModel, bool initStates)
        {
            _finishButtonModel = finishButtonModel;
        }

        public void HideButton()
        {
            _finishButtonModel.IsVisible = false;
        }

        public void ShowButton()
        {
            _finishButtonModel.IsVisible = true;
        }
    }
}
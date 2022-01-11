using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class ProfileLayer : AbstractLayer
    {
        public ProfileLayer(ProfilePresenter presenter, ProfilesHandler handler, string title = "Profiles layer") : base(presenter, handler, title)
        {
        }
    }
}
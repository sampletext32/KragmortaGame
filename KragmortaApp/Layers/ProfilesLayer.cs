using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class ProfilesLayer : AbstractLayer
    {
        public ProfilesLayer(ProfilesPresenter presenter, ProfilesHandler handler, string title = "Profiles layer") : base(presenter, handler, title)
        {
        }
    }
}
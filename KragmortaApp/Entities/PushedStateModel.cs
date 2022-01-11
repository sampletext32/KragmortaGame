using System.Collections.Generic;
using System.Linq;
using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
{
    public class PushedStateModel : VisualEntity
    {
        public HeroModel Pusher;
        public HeroModel Victim;

        public bool ShouldReturnMoveToPusher;

        public PushedStateModel(PushedStateFileData fileData, List<HeroModel> heroes)
        {
            if (fileData.Pusher is not null)
            {
                Pusher = heroes.First(h => h.Id == fileData.Pusher.Value);
            }

            if (fileData.Victim is not null)
            {
                Victim = heroes.First(h => h.Id == fileData.Victim.Value);
            }

            ShouldReturnMoveToPusher = fileData.ShouldReturnMoveToPusher;
        }

        public PushedStateModel()
        {
        }

        public void SetFromHeroPair(HeroModel pusher, HeroModel victim, bool shouldReturnMoveToPusher)
        {
            Pusher                   = pusher;
            Victim                   = victim;
            ShouldReturnMoveToPusher = shouldReturnMoveToPusher;
        }

        public PushedStateFileData ToFileData()
        {
            return new PushedStateFileData()
            {
                Pusher                   = Pusher?.Id,
                Victim                   = Victim?.Id,
                ShouldReturnMoveToPusher = ShouldReturnMoveToPusher
            };
        }
    }
}
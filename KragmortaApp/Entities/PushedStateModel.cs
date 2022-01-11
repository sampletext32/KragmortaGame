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
            Pusher                   = heroes.First(h => h.Id == fileData.Pusher);
            Victim                   = heroes.First(h => h.Id == fileData.Victim);
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
                Pusher                   = Pusher.Id,
                Victim                   = Victim.Id,
                ShouldReturnMoveToPusher = ShouldReturnMoveToPusher
            };
        }
    }
}
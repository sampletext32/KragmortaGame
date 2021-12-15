namespace KragmortaApp.Entities
{
    public class PushedStateModel : VisualEntity
    {
        public HeroModel Pusher;
        public HeroModel Victim;

        public bool PusherContinuesTurn;

        public void SetFromHeroPair(HeroModel pusher, HeroModel victim, bool pusherContinuesTurn)
        {
            Pusher                   = pusher;
            Victim                   = victim;
            PusherContinuesTurn = pusherContinuesTurn;
        }
    }
}
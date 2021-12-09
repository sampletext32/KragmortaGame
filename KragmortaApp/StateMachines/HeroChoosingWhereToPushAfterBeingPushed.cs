using KragmortaApp.Entities;

namespace KragmortaApp.StateMachines
{
    public class HeroChoosingWhereToPushAfterBeingPushed : AbstractHeroState
    {
        public HeroModel Pusher { get; }

        public HeroChoosingWhereToPushAfterBeingPushed(HeroModel pusher)
        {
            Pusher = pusher;
        }

        public override void OnExitTo(HeroChoosingWhereToMove1State state)
        {
        }

        public override void OnExitTo(HeroChoosingWhereToMove2State state)
        {
        }

        public override void OnExitTo(HeroChoosingWhereToPushAfterMove1State state)
        {
        }

        public override void OnExitTo(HeroChoosingWhereToPushAfterMove2State state)
        {
        }

        public override void OnExitTo(HeroChoosingWhereToPushAfterBeingPushed state)
        {
        }

        public override void OnExitTo(HeroWaitingForSecondMoveState state)
        {
        }

        public override void OnExitTo(HeroWaitingForTurnState state)
        {
        }

        public override void OnEnter()
        {
        }
    }
}
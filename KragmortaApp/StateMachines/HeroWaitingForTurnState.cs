using System;

namespace KragmortaApp.StateMachines
{
    public class HeroWaitingForTurnState : AbstractHeroState
    {
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
            Console.WriteLine("Pushed by " + state.Pusher.Nickname);
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
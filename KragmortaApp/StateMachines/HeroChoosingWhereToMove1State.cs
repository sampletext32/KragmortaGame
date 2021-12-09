using System;

namespace KragmortaApp.StateMachines
{
    public class HeroChoosingWhereToMove1State : AbstractHeroState
    {
        public override void OnExitTo(HeroChoosingWhereToMove1State state)
        {
            throw new KragException("Cant make a transition");
        }

        public override void OnExitTo(HeroChoosingWhereToMove2State state)
        {
            Console.WriteLine("Successfully moved to second move");
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
using System;

namespace KragmortaApp.StateMachines
{
    public class HeroChoosingWhereToMove2State : AbstractHeroState
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
        }

        public override void OnExitTo(HeroWaitingForSecondMoveState state)
        {
        }

        public override void OnExitTo(HeroWaitingForTurnState state)
        {
            Console.WriteLine("Finished second move");
        }

        public override void OnEnter()
        {
        }
    }
}
﻿namespace KragmortaApp.StateMachines
{
    public class HeroWaitingForSecondMoveState : AbstractHeroState
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
        }

        public override void OnEnter()
        {
        }
    }
}
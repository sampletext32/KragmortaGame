namespace KragmortaApp.StateMachines
{
    public abstract class AbstractHeroState
    {
        public abstract void OnExitTo(HeroChoosingWhereToMove1State state);
        public abstract void OnExitTo(HeroChoosingWhereToMove2State state);
        public abstract void OnExitTo(HeroChoosingWhereToPushAfterMove1State state);
        public abstract void OnExitTo(HeroChoosingWhereToPushAfterMove2State state);
        public abstract void OnExitTo(HeroChoosingWhereToPushAfterBeingPushed state);
        public abstract void OnExitTo(HeroWaitingForSecondMoveState state);
        public abstract void OnExitTo(HeroWaitingForTurnState state);

        public abstract void OnEnter();
    }
}
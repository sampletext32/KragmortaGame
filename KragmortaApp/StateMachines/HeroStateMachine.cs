namespace KragmortaApp.StateMachines
{
    public class HeroStateMachine
    {
        public AbstractHeroState CurrentState => _currentState;
        
        private AbstractHeroState _currentState;

        public HeroStateMachine(AbstractHeroState initialState)
        {
            _currentState = initialState;
        }

        public void Transition(HeroChoosingWhereToMove1State state)
        {
            _currentState.OnExitTo(state);
            _currentState = state;
            _currentState.OnEnter();
        }

        public void Transition(HeroChoosingWhereToMove2State state)
        {
            _currentState.OnExitTo(state);
            _currentState = state;
            _currentState.OnEnter();
        }

        public void Transition(HeroWaitingForTurnState state)
        {
            _currentState.OnExitTo(state);
            _currentState = state;
            _currentState.OnEnter();
        }

        public void Transition(HeroChoosingWhereToPushAfterBeingPushed state)
        {
            _currentState.OnExitTo(state);
            _currentState = state;
            _currentState.OnEnter();
        }
    }
}
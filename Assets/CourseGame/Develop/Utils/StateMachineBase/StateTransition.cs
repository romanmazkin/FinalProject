using Assets.CourseGame.Develop.Utils.Conditions;

namespace Assets.CourseGame.Develop.Utils.StateMachineBase
{
    public class StateTransition<TState> where TState : IState
    {
        public StateTransition(StateNode<TState> toState, ICondition condition)
        {
            ToState = toState;
            Condition = condition;
        }

        public StateNode<TState> ToState {  get; }
        public ICondition Condition { get; }
    }
}

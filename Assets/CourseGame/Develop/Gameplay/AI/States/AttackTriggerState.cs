using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;

namespace Assets.CourseGame.Develop.Gameplay.AI.States
{
    public class AttackTriggerState : State, IUpdatableState
    {
        private ReactiveEvent _attackRequest;

        public AttackTriggerState(ReactiveEvent attackRequest)
        {
            _attackRequest = attackRequest;
        }

        public override void Enter()
        {
            base.Enter();

            _attackRequest?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}

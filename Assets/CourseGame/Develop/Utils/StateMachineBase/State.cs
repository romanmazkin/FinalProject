using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Utils.StateMachineBase
{
    public abstract class State : IState
    {
        private ReactiveEvent _entered = new ReactiveEvent();

        private ReactiveEvent _exited = new ReactiveEvent();

        public IReadOnlyEvent Entered => _entered;

        public IReadOnlyEvent Exited => _exited;

        public virtual void Enter() => _entered?.Invoke();

        public virtual void Exit() => _exited?.Invoke();
    }
}

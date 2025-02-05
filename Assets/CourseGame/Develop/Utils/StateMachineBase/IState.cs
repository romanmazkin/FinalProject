using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Utils.StateMachineBase
{
    public interface IState
    {
        IReadOnlyEvent Entered {  get; }

        IReadOnlyEvent Exited {  get; }

        void Enter();

        void Exit();
    }
}

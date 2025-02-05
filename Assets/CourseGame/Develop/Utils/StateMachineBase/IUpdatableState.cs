namespace Assets.CourseGame.Develop.Utils.StateMachineBase
{
    public interface IUpdatableState : IState
    {
        void Update(float deltaTime);
    }
}

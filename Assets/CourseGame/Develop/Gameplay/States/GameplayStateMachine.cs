using Assets.CourseGame.Develop.Utils.StateMachineBase;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class GameplayStateMachine : StateMachine<IUpdatableState>
    {
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            CurrentState.State.Update(deltaTime);
        }
    }
}

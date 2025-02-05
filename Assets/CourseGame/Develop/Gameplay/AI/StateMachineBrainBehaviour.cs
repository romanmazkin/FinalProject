using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;

namespace Assets.CourseGame.Develop.Gameplay.AI
{
    public class StateMachineBrainBehaviour : IEntityInitialize, IEntityDispose, IEntityUpdate
    {
        private AIStateMachine _stateMachine;

        public StateMachineBrainBehaviour(AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnInit(Entity entity) => _stateMachine.Enter();

        public void OnDispose()
        {
            _stateMachine.Exit();

            _stateMachine.Dispose();
        }

        public void OnUpdate(float deltaTime) => _stateMachine.Update(deltaTime);
    }
}

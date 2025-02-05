using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class AttackCancelBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<bool> _isAttackProcess;

        private ICondition _canceledCondition;

        public void OnInit(Entity entity)
        {
            _isAttackProcess = entity.GetIsAttackProcess();
            _canceledCondition = entity.GetAttackCanceledCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isAttackProcess.Value == false)
                return;

            if (_canceledCondition.Evaluate())
                _isAttackProcess.Value = false;
        }
    }
}
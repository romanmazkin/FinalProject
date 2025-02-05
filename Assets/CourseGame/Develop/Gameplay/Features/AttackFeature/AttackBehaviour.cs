using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class AttackBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _attackTrigger;
        private ReactiveVariable<bool> _isAttackProcess;

        private ICondition _attackCondition;
        private IDisposable _attackTriggerDispose;

        public void OnInit(Entity entity)
        {
            _attackTrigger = entity.GetAttackTrigger();
            _attackCondition = entity.GetAttackCondition();
            _isAttackProcess = entity.GetIsAttackProcess();
            _attackTriggerDispose = _attackTrigger.Subscribe(OnAttackTrigger);
        }

        private void OnAttackTrigger()
        {
            if (_attackCondition.Evaluate())
                _isAttackProcess.Value = true;
        }

        public void OnDispose()
        {
            _attackTriggerDispose.Dispose();
        }
    }
}
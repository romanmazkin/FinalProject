using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class RestartAttackCooldownOnInstantAttackBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<float> _attackInterval;
        private ReactiveVariable<float> _attackCooldown;

        private ReactiveEvent _instantAttackEvent;

        private IDisposable _disposableInstantAttackEvent;

        public void OnInit(Entity entity)
        {
            _attackInterval = entity.GetIntervalBetweenAttacks();
            _attackCooldown = entity.GetAttackCooldown();
            _instantAttackEvent = entity.GetInstantAttackEvent();
            _disposableInstantAttackEvent = _instantAttackEvent.Subscribe(OnInstantAtackEvent);
        }

        private void OnInstantAtackEvent() => _attackCooldown.Value = _attackInterval.Value;

        public void OnDispose()
        {
            _disposableInstantAttackEvent.Dispose();
        }
    }
}
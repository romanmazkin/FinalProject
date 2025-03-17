using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class InstantShootBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _attackEvent;
        private ReactiveVariable<float> _damage;
        private Transform _shootPoint;
        private IDisposable _disposableAttackEvent;
        private EntityFactory _entityFactory;
        private Entity _entity;

        public InstantShootBehaviour(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _attackEvent = entity.GetInstantAttackEvent();
            _damage = entity.GetDamage();
            _shootPoint = entity.GetShootPoint();
            _disposableAttackEvent = _attackEvent.Subscribe(OnAttackEvent);
        }

        private void OnAttackEvent()
        {
            _entityFactory.CreateArrow(_shootPoint.position, _shootPoint.forward, _damage.Value, _entity);
        }

        public void OnDispose()
        {
            _disposableAttackEvent.Dispose();
        }
    }
}
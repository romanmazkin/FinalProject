using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class DirectionsInstantShootingBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _attackEvent;
        private InstantShootingDirectionArgs _directions;
        private ReactiveVariable<float> _damage;
        private Transform _shootPoint;
        private Entity _entity;

        private IDisposable _disposableAttackEvent;

        private EntityFactory _entityFactory;

        public DirectionsInstantShootingBehaviour(EntityFactory factory)
        {
            _entityFactory = factory;   
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _attackEvent = entity.GetInstantAttackEvent();
            _damage = entity.GetDamage();   
            _shootPoint = entity.GetShootPoint();
            _directions = entity.GetInstanShootingDirections();

            _disposableAttackEvent = _attackEvent.Subscribe(OnAttack);
        }

        private void OnAttack()
        {
            foreach (var arg in _directions.Args)
                Shoot(arg.Angel, arg.ProjectileCounts);
        }

        private void Shoot(int angel, int projectileCounts)
        {
            Vector3 directionForShoot = Quaternion.Euler(new Vector3(0, angel, 0)) * _shootPoint.forward;
            Vector2 perpindicular = Vector2.Perpendicular(new Vector2(directionForShoot.x, directionForShoot.z)).normalized;

            float offesetBetweenProjectiles = 0.6f;

            for (int i = 0; i < projectileCounts; i++)
            {
                Vector2 offset = perpindicular * (-offesetBetweenProjectiles / 2f * (projectileCounts - 1) + i * offesetBetweenProjectiles);
                Vector3 position = new Vector3(_shootPoint.position.x + offset.x, _shootPoint.position.y, _shootPoint.position.z + offset.y);

                _entityFactory.CreateArrow(position, directionForShoot, _damage.Value, _entity);
            }
        }

        public void OnDispose()
        {
            _disposableAttackEvent.Dispose();
        }
    }
}

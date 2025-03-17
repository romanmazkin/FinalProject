using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    public class GenerateMoveDirectionTargetBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<Entity> _target;
        private Transform _transform;
        private ReactiveVariable<Vector3> _moveDirection;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _transform = entity.GetTransform();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_target.Value != null)
                _moveDirection.Value = _target.Value.GetTransform().position - _transform.position;
            else
                _moveDirection.Value = Vector3.zero;
        }
    }
}
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    public class TransformMovementBehaviour : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<Vector3> _direction;
        private IReadOnlyVariable<float> _speed;
        private ReactiveVariable<bool> _isMoving;
        private ICondition _moveCodition;

        public void OnInit(Entity entity)
        {
            _speed = entity.GetMoveSpeed();
            _transform = entity.GetTransform();
            _direction = entity.GetMoveDirection();
            _isMoving = entity.GetIsMoving();
            _moveCodition = entity.GetMoveCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_moveCodition.Evaluate() == false)
                return;

            Vector3 velocity = _direction.Value.normalized * _speed.Value;

            _isMoving.Value = velocity.magnitude > 0; //можно написать поведение того движемся мы или нет

            _transform.Translate(velocity * deltaTime);
        }
    }
}
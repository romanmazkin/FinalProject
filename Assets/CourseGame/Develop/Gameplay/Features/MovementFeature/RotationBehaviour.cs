using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    public class RotationBehaviour : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<float> _rotationSpeed;
        private IReadOnlyVariable<Vector3> _direction;

        private ICondition _condition;

        public void OnInit(Entity entity)
        {
            _rotationSpeed = entity.GetRotationSpeed();
            _direction = entity.GetRotationDirection();
            _transform = entity.GetTransform();
            _condition = entity.GetRotationCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
                return;

            if (_direction.Value == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_direction.Value.normalized);
            float step = _rotationSpeed.Value * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
        }
    }
}


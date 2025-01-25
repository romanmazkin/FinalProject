using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    public class RotationBehaviour : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<float> _rotationSpeed;
        private IReadOnlyVariable<Vector3> _direction;

        public void OnInit(Entity entity)
        {
            _rotationSpeed = entity.GetValue<ReactiveVariable<float>>(EntityValues.RotationSpeed);
            _direction = entity.GetValue<ReactiveVariable<Vector3>>(EntityValues.RotationDirection);
            _transform = entity.GetValue<Transform>(EntityValues.Transform);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_direction.Value == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_direction.Value.normalized);
            float step = _rotationSpeed.Value * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
        }
    }
}

using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    public class ForceRotationBehaviour : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<Vector3> _direction;
        private ICondition _condition;

        public void OnInit(Entity entity)
        {
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

            _transform.rotation = Quaternion.LookRotation(_direction.Value);
        }
    }
}
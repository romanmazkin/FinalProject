using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.BounceFeature
{
    public class ReflectRotationDirectionOnBounceBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<Vector3> _rotationDirection;
        private ReactiveEvent<RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;

        public void OnInit(Entity entity)
        {
            _rotationDirection = entity.GetRotationDirection();
            _bounceEvent = entity.GetBounceEvent();
            _bounceDisposable = _bounceEvent.Subscribe(OnBounce);
        }

        private void OnBounce(RaycastHit hit)
        {
            _rotationDirection.Value = Vector3.Reflect(_rotationDirection.Value, hit.normal);
        }

        public void OnDispose()
        {
            _bounceDisposable.Dispose();
        }
    }
}

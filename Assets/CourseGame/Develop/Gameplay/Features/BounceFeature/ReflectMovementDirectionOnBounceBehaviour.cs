using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.BounceFeature
{
    internal class ReflectMovementDirectionOnBounceBehaviour : IEntityInitialize, IEntityDispose
    {
        private Transform _transform;
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveEvent<RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;

        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _movementDirection = entity.GetMoveDirection();
            _bounceEvent = entity.GetBounceEvent();

            _bounceDisposable = _bounceEvent.Subscribe(OnBounceEvent);
        }

        private void OnBounceEvent(RaycastHit hit)
        {
            _movementDirection.Value = Vector3.Reflect(_movementDirection.Value, hit.normal);
            _transform.position = hit.point + hit.normal * 0.1f;
        }

        public void OnDispose()
        {
            _bounceDisposable.Dispose();
        }
    }
}

using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.BounceFeature
{
    public class BounceCountDecreaseBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<int> _bounceCount;
        private ReactiveEvent<RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;

        public void OnInit(Entity entity)
        {
            _bounceCount = entity.GetBounceCount();
            _bounceEvent = entity.GetBounceEvent();

            _bounceDisposable = _bounceEvent.Subscribe(OnBounceEvent);
        }

        private void OnBounceEvent(RaycastHit hit)
        {
            _bounceCount.Value--;
        }

        public void OnDispose()
        {
            _bounceDisposable.Dispose();
        }
    }
}

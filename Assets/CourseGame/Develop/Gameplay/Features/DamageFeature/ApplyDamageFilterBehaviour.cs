using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.DamageFeature
{
    public class ApplyDamageFilterBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _takeDamageEvent;
        private ReactiveEvent<float> _takeDamageRequest;
        private ICondition _takeDamageCondition;

        private IDisposable _disposableTakeDamageRequest;

        public void OnInit(Entity entity)
        {
            _takeDamageCondition = entity.GetTakeDamageCondition();
            _takeDamageRequest = entity.GetTakeDamageRequest();
            _takeDamageEvent = entity.GetTakeDamageEvent();

            _disposableTakeDamageRequest = _takeDamageRequest.Subscribe(OnTakeDamageRequest);
        }

        private void OnTakeDamageRequest(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if (_takeDamageCondition.Evaluate())
                _takeDamageEvent.Invoke(damage);
        }

        public void OnDispose()
        {
            _disposableTakeDamageRequest.Dispose();
        }
    }
}


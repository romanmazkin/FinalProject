using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.DamageFeature
{
    public class ApplyDamageBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _takeDamageEvent;
        private ReactiveVariable<float> _health;

        private IDisposable _disposableTakeDamage;

        public void OnInit(Entity entity)
        {
            _takeDamageEvent = entity.GetTakeDamageEvent();
            _health = entity.GetHealth();

            _disposableTakeDamage = _takeDamageEvent.Subscribe(OnTakeDamage);
        }

        private void OnTakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            float tempHealth = _health.Value - damage;
            _health.Value = Math.Max(tempHealth, 0);
        }

        public void OnDispose()
        {
            _disposableTakeDamage.Dispose();
        }
    }
}


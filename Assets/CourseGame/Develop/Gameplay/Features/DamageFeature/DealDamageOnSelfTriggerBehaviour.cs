using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Extensions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DamageFeature
{
    public class DealDamageOnSelfTriggerBehaviour : IEntityInitialize, IEntityDispose
    {
        private TriggerReciever _triggerReciever;
        private ReactiveVariable<float> _damage;
        private ReactiveVariable<int> _team;


        private IDisposable _disposableTriggerEnter;

        public void OnInit(Entity entity)
        {
            _triggerReciever = entity.GetSelfTriggerReciever();
            _damage = entity.GetSelfTriggerDamage();
            _team = entity.GetTeam();

            _disposableTriggerEnter = _triggerReciever.Enter.Subscribe(OnTriggerEnter);
        }

        private void OnTriggerEnter(Collider collider)
        {
            Entity otherEntity = collider.GetComponentInParent<Entity>();

            if (otherEntity != null)
            {
                Debug.Log("НАШЕЛ ВРАГА, НАНОШУ УРОН");
                otherEntity.TryTakeDamage(_damage.Value, _team.Value);
            }
        }

        public void OnDispose()
        {
            _disposableTriggerEnter.Dispose();
        }
    }
}
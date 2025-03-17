using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    public class DisableCollidersOnDeathBehaviour : IEntityInitialize, IEntityDispose
    {
        private IEnumerable<Collider> _colliders;
        private ReactiveVariable<bool> _isDead;

        public void OnInit(Entity entity)
        {
            _isDead = entity.GetIsDead();
            _colliders = entity.GetCollidersDisabledOnDeath();
            _isDead.Changed += OnDeadChanged;
        }

        private void OnDeadChanged(bool oldValue, bool isDead)
        {
            if (isDead)
                foreach (Collider collider in _colliders)
                    collider.enabled = false;
        }

        public void OnDispose()
        {
            _isDead.Changed -= OnDeadChanged;
        }
    }
}

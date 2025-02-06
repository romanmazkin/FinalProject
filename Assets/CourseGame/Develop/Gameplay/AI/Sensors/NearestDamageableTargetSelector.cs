using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.CourseGame.Develop.Gameplay.AI.Sensors
{
    public class NearestDamageableTargetSelector : ITargetSelector
    {
        private Transform _center;
        private ReactiveVariable<int> _team;

        public NearestDamageableTargetSelector(Transform center, ReactiveVariable<int> team)
        {
            _center = center;
            _team = team;
        }

        public bool TrySelectTarget(IEnumerable<Entity> targets, out Entity findedTarget)
        {
            IEnumerable<Entity> damageableTargets = targets
                .Where(target =>
                    target.TryGetTakeDamageRequest(out var request)
                    && target.TryGetIsDead(out var isDead)
                    && isDead.Value == false
                    && target.TryGetTeam(out ReactiveVariable<int> team)
                    && team.Value != _team.Value);

            if (damageableTargets.Any() == false)
            {
                findedTarget = null;
                return false;
            }

            Entity closestTarget = damageableTargets.First();
            float minDistance = GetDistanceTo(closestTarget);

            foreach (Entity entity in damageableTargets)
            {
                float distance = GetDistanceTo(entity);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = entity;
                }
            }

            findedTarget = closestTarget;
            return true;
        }

        private float GetDistanceTo(Entity target) => (_center.position - target.GetTransform().position).magnitude;
    }
}

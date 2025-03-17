using Assets.CourseGame.Develop.Gameplay.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    public class CollidersDisabledOnDeathRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private List<Collider> _colliders;

        public override void Register(Entity entity)
        {
            entity.AddCollidersDisabledOnDeath(_colliders);
        }
    }
}

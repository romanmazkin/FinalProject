using Assets.CourseGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class ShootPointEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _shootPoint;

        public override void Register(Entity entity)
        {
            entity.AddShootPoint(_shootPoint);
        }
    }
}
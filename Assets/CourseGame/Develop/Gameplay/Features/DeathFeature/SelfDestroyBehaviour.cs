using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using UnityEngine;


namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    public class SelfDestroyBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ICondition _selfDestroyCondition;
        private Transform _entityTransform;

        public void OnInit(Entity entity)
        {
            _entityTransform = entity.GetTransform();
            _selfDestroyCondition = entity.GetSelfDestroyCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_selfDestroyCondition.Evaluate())
                Object.Destroy(_entityTransform.gameObject);
        }
    }
}

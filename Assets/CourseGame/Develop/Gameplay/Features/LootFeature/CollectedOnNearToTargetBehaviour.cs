using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class CollectedOnNearToTargetBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<Entity> _target;
        private Transform _transform;
        private ReactiveVariable<bool> _isCollected;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _transform = entity.GetTransform();
            _isCollected = entity.GetIsCollected();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isCollected.Value == false && _target.Value != null)
                if ((_target.Value.GetTransform().position - _transform.position).magnitude < 0.1f)
                    _isCollected.Value = true;
        }
    }
}

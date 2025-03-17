using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class CollectHealthToTargetBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<bool> _isCollected;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _health = entity.GetHealth();
            _isCollected = entity.GetIsCollected();

            _isCollected.Changed += OnIsCollectedChanged;
        }

        private void OnIsCollectedChanged(bool arg1, bool isCollected)
        {
            if (isCollected)
            {
                var currentHealth = _target.Value.GetHealth();

                if (currentHealth.Value + _health.Value > _target.Value.GetMaxHealth().Value)
                    return;

                currentHealth.Value += _health.Value;
            }
        }

        public void OnDispose()
        {
            _isCollected.Changed -= OnIsCollectedChanged;
        }
    }
}

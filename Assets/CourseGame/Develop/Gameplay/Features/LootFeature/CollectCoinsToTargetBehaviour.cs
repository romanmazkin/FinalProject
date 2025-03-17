using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class CollectCoinsToTargetBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<int> _coins;
        private ReactiveVariable<bool> _isCollected;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _coins = entity.GetCoins();
            _isCollected = entity.GetIsCollected();

            _isCollected.Changed += OnIsCollectedChanged;
        }

        private void OnIsCollectedChanged(bool arg1, bool isCollected)
        {
            if (isCollected)
                _target.Value.GetCoins().Value += _coins.Value;
        }

        public void OnDispose()
        {
            _isCollected.Changed -= OnIsCollectedChanged;
        }
    }
}
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class EndAttackCooldownOnMovingBehaviour : IEntityDispose, IEntityInitialize
    {
        private ReactiveVariable<bool> _isMoving;
        private ReactiveVariable<float> _attackCooldown;

        public void OnInit(Entity entity)
        {
            _isMoving = entity.GetIsMoving();
            _attackCooldown = entity.GetAttackCooldown();

            _isMoving.Changed += OnMovingChanged;
        }

        private void OnMovingChanged(bool arg1, bool isMoving)
        {
            if (isMoving)
                _attackCooldown.Value = 0;
        }

        public void OnDispose()
        {
            _isMoving.Changed -= OnMovingChanged;
        }
    }
}
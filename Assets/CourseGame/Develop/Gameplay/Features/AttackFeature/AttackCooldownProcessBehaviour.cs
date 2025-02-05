using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class AttackCooldownProcessBehaviour : IEntityUpdate, IEntityInitialize
    {
        private ReactiveVariable<float> _attackCooldown;

        public void OnInit(Entity entity)
        {
            _attackCooldown = entity.GetAttackCooldown();
        }

        public void OnUpdate(float deltaTime)
        {
            if (CooldownIsOver() == false)
                _attackCooldown.Value -= deltaTime;
        }

        private bool CooldownIsOver() => _attackCooldown.Value <= 0;
    }
}
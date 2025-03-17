using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    public class InstantDeathBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ICondition _condition;
        private ReactiveVariable<bool> _isDead;

        public void OnInit(Entity entity)
        {
            _condition = entity.GetDeathCondition();
            _isDead = entity.GetIsDead();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_condition.Evaluate())
                _isDead.Value = true;
        }
    }
}

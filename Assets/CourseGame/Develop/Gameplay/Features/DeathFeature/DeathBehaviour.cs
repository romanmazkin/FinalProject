using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    public class DeathBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ICondition _condition;
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _isDeathProcess;

        public void OnInit(Entity entity)
        {
            _isDead = entity.GetIsDead();
            _condition = entity.GetDeathCondition();
            _isDeathProcess = entity.GetIsDeathProcess();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_condition.Evaluate())
            {
                _isDead.Value = true;
                _isDeathProcess.Value = true;
            }
        }
    }
}


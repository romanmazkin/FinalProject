using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.Features.StatsFeature
{
    public class AttackIntervalModifierApplierBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _attackInterval;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _attackInterval = entity.GetIntervalBetweenAttacks();
            _modifiedStats = entity.GetModifiedStats();
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.AttackInterval];

            if (tempValue < 0.3)
                tempValue = 0.3f;

            _attackInterval.Value = tempValue;
        }
    }
}

using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.Features.StatsFeature
{
    public class MoveSpeedModifierApplierBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _speed;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _speed = entity.GetMoveSpeed();
            _modifiedStats = entity.GetModifiedStats();
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.MoveSpeed];

            if (tempValue < 0)
                tempValue = 0;

            _speed.Value = tempValue;
        }
    }
}

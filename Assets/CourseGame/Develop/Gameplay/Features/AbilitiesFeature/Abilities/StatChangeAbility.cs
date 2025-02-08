using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class StatChangeAbility : IAbility
    {
        private Entity _entity;
        private StatChangeAbilityConfig _config;

        public StatChangeAbility(Entity entity, StatChangeAbilityConfig config)
        {
            _entity = entity;
            _config = config;
        }

        public string ID => _config.ID;

        public void Activate()
        {
            _entity.GetStatsEffectsList().Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}

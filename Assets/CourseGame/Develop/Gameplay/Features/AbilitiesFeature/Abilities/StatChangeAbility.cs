using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class StatChangeAbility : Ability
    {
        private Entity _entity;
        private StatChangeAbilityConfig _config;

        public StatChangeAbility(Entity entity, StatChangeAbilityConfig config, int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.GetStatsEffectsList().Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}

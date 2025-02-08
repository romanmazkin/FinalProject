using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions;
using Assets.CourseGame.Develop.Gameplay.Entities;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.AbilityDropServiceFeature
{
    public class AbilityDropingRules
    {
        public bool IsAvailable(AbilityDropOption dropOption, Entity entity)
        {
            switch (dropOption.Config)
            {
                case StatChangeAbilityConfig statChangeAbilityConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                        && modifiedStats.ContainsKey(statChangeAbilityConfig.StatType);
            }

            return false;
        }
    }
}

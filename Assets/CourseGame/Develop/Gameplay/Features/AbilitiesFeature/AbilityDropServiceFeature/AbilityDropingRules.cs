using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Extensions;
using System.Linq;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.AbilityDropServiceFeature
{
    public class AbilityDropingRules
    {
        public bool IsAvailable(AbilityDropOption dropOption, Entity entity)
        {
            if (dropOption.Config.IsUpgradable())
            {
                if (entity.GetAbilityList().Elements.Any(ability =>
                ability.ID == dropOption.Config.ID
                && ability.CurrentLevel.Value + dropOption.Level > ability.MaxLevel))
                {
                    return false;
                }
            }

            switch (dropOption.Config)
            {
                case StatChangeAbilityConfig statChangeAbilityConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                        && modifiedStats.ContainsKey(statChangeAbilityConfig.StatType);
            }

            return true;
        }
    }
}

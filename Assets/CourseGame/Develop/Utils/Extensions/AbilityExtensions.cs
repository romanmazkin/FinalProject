using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature;

namespace Assets.CourseGame.Develop.Utils.Extensions
{
    public static class AbilityExtensions
    {
        public static bool IsUpgradable(this Ability ability)
            => IsUpgradable(ability.MaxLevel);

        public static bool IsUpgradable(this AbilityConfig config)
            => IsUpgradable(config.MaxLevel);

        private static bool IsUpgradable(int maxLevel) => maxLevel > 1;
    }
}

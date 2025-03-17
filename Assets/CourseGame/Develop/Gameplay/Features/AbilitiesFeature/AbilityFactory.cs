using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Abilities;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature
{
    public class AbilityFactory
    {
        private DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public Ability CreateAbilityFor(Entity entity, AbilityConfig config, int currentLevel)
        {
            switch (config)
            {
                case StatChangeAbilityConfig changeAbilityConfig:
                    return new StatChangeAbility(entity, changeAbilityConfig, currentLevel);

                case AdditionalDirectionsShotAbilityConfig additionalDirectionsShotAbilityConfig:
                    return new AdditionalDirectionsShotAbility(additionalDirectionsShotAbilityConfig, entity, currentLevel);

                case BounceProjectileAbilityConfig bounceProjectileAbilityConfig:
                    return new BounceProjectileAbility(bounceProjectileAbilityConfig, entity, _container.Resolve<EntitiesBuffer>(), currentLevel);

                default:
                    throw new ArgumentException();
            }
        }
    }
}

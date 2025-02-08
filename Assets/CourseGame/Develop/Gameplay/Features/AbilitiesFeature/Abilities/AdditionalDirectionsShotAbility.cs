using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AttackFeature;
using System;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class AdditionalDirectionsShotAbility : Ability, IDisposable
    {
        private AdditionalDirectionsShotAbilityConfig _config;
        private Entity _entity;

        public AdditionalDirectionsShotAbility(
            AdditionalDirectionsShotAbilityConfig config,
            Entity entity,
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _entity = entity;
        }

        public override void Activate()
        {
            for (int i = 0; i < CurrentLevel.Value; i++)
            {
                AddShotDirectionsBy(i + 1);
            }

            CurrentLevel.Changed += OnCurrentLevelChaged;
        }

        private void OnCurrentLevelChaged(int arg1, int newLevel)
        {
            AddShotDirectionsBy(newLevel);
        }

        private void AddShotDirectionsBy(int level)
        {
            List<DirectionShotConfig> directionShotConfigs = _config.GetBy(level);

            InstantShootingDirectionArgs shootingArgs = _entity.GetInstanShootingDirections();

            foreach(var directionShotConfig in directionShotConfigs)
            {
                shootingArgs.Add(new InstantShotDirectionArgs(directionShotConfig.Angel, directionShotConfig.NumberOfProjectiles));
            }
        }

        public void Dispose()
        {
            CurrentLevel.Changed -= OnCurrentLevelChaged;
        }
    }
}

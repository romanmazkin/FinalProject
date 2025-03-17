using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.BounceFeature;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class BounceProjectileAbility : Ability, IDisposable
    {
        private BounceProjectileAbilityConfig _config;
        private Entity _owner;
        private EntitiesBuffer _creaturesBuffer;

        public BounceProjectileAbility(
            BounceProjectileAbilityConfig config,
            Entity owner,
            EntitiesBuffer creaturesBuffer,
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _owner = owner;
            _creaturesBuffer = creaturesBuffer;
        }

        public override void Activate()
        {
            _creaturesBuffer.Added += OnCreaturesAdded;
        }

        private void OnCreaturesAdded(Entity entity)
        {
            if(entity.TryGetIsProjectile(out bool isProjectile) && isProjectile
                && entity.TryGetOwner(out Entity projectileOwner) && projectileOwner == _owner)
            {
                entity
                    .AddBounceCount(new ReactiveVariable<int>(_config.GetBounceCountBy(CurrentLevel.Value)))
                    .AddBounceEvent()
                    .AddLayerToBounceReaction(_config.LayerBounceRection);

                entity
                    .GetDeathCondition()
                    .Add(new FuncCondition(() => entity.GetBounceCount().Value + 1 == 0), 5);

                entity
                    .AddBehaviour(new BounceDetectorBehaviour())
                    .AddBehaviour(new ReflectRotationDirectionOnBounceBehaviour())
                    .AddBehaviour(new ReflectMovementDirectionOnBounceBehaviour())
                    .AddBehaviour(new BounceCountDecreaseBehaviour());
            }
        }

        public void Dispose()
        {
            _creaturesBuffer.Added -= OnCreaturesAdded;
        }
    }
}

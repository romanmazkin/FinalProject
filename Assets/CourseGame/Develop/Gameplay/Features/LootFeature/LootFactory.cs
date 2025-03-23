using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class LootFactory
    {
        private EntityFactory _entityFactory;
        private EntitiesBuffer _entitiesBuffer;

        public LootFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();    
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
        }

        public Entity CreateExperienceLoot(Entity prefab, Vector3 position, float experience)
        {
            Entity pullableBase = _entityFactory.CreatePullable(prefab, position);

            pullableBase
                .AddExperience(new ReactiveVariable<float>(experience))
                .AddBehaviour(new CollectExperienceToTargetBehaviour());

            _entitiesBuffer.Add(pullableBase);

            return pullableBase;
        }

        public Entity CreateCoinsLoot(Entity prefab, Vector3 position, int coins)
        {
            Entity pullableBase = _entityFactory.CreatePullable(prefab, position);

            pullableBase
                .AddCoins(new ReactiveVariable<int>(coins))
                .AddBehaviour(new CollectCoinsToTargetBehaviour());

            _entitiesBuffer.Add(pullableBase);

            return pullableBase;
        }

        public Entity CreateHealthLoot(Entity prefab, Vector3 position, float health)
        {
            Entity pullableBase = _entityFactory.CreatePullable(prefab, position);

            pullableBase
                .AddHealth(new ReactiveVariable<float>(health))
                .AddBehaviour(new CollectHealthToTargetBehaviour());

            _entitiesBuffer.Add(pullableBase);

            return pullableBase;
        }
    }
}

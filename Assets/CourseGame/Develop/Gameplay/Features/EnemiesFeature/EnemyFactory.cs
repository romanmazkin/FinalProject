using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Configs.Gameplay.Creatures;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.LootFeature;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using Assets.CourseGame.Develop.Utils.Conditions;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature
{
    public class EnemyFactory
    {
        private const string LightGhostPrefabPath = "Gameplay/Creatures/GhostLight";
        private const string MediumGhostPrefabPath = "Gameplay/Creatures/GhostMedium";
        private const string HardGhostPrefabPath = "Gameplay/Creatures/GhostHard";
        private const string ExtraHardGhostPrefabPath = "Gameplay/Creatures/GhostExtraHard";
        private const string BossGhostPrefabPath = "Gameplay/Creatures/GhostBoss";
        private readonly int _team = TeamTypes.Enemies;

        private EntityFactory _entityFactory;
        private AIFactory _aIFactory;

        private EntitiesBuffer _entitiesBuffer;
        private DropLootService _dropLootService;

        public EnemyFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _aIFactory = container.Resolve<AIFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _dropLootService = container.Resolve<DropLootService>();
        }

        public Entity Create(Vector3 position, CreatureConfig config)
        {
            Entity entity;
            AIStateMachine brain;

            switch (config)
            {
                case LightGhostConfig lightGhostConfig:
                    entity = _entityFactory.CreateGhost(position, lightGhostConfig, _team, LightGhostPrefabPath);
                    brain = _aIFactory.CreateGhostBehaviour(entity);
                    break;

                case MediumGhostConfig mediumGhostConfig:
                    entity = _entityFactory.CreateGhost(position, mediumGhostConfig, _team, MediumGhostPrefabPath);
                    brain = _aIFactory.CreateGhostBehaviour(entity);
                    break;

                case HardGhostConfig hardGhostConfig:
                    entity = _entityFactory.CreateGhost(position, hardGhostConfig, _team, HardGhostPrefabPath);
                    brain = _aIFactory.CreateGhostBehaviour(entity);
                    break;

                case ExtraHardGhostConfig extraHardGhostConfig:
                    entity = _entityFactory.CreateGhost(position, extraHardGhostConfig, _team, ExtraHardGhostPrefabPath);
                    brain = _aIFactory.CreateGhostBehaviour(entity);
                    break;

                case BossGhostConfig bossGhostConfig:
                    entity = _entityFactory.CreateGhost(position, bossGhostConfig, _team, BossGhostPrefabPath);
                    brain = _aIFactory.CreateGhostBehaviour(entity);
                    break;

                default:
                    throw new ArgumentException("Wrong enemy config.");
            }

            entity.AddBehaviour(new StateMachineBrainBehaviour(brain));

            AddDropLootBehaviourTo(entity);

            _entitiesBuffer.Add(entity);

            return entity;
        }

        private void AddDropLootBehaviourTo(Entity entity)
        {
            ICompositeCondition dropLootCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => entity.GetIsDead().Value))
                .Add(new FuncCondition(() => entity.GetLootIsDropped().Value == false));

            entity
                .AddLootIsDropped()
                .AddDropLootCondition(dropLootCondition);

            entity.GetSelfDestroyCondition()
                .Add(new FuncCondition(() => entity.GetLootIsDropped().Value));

            entity.AddBehaviour(new DropLootBehaviour(_dropLootService));
        }
    }
}

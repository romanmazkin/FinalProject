using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.Configs.Gameplay.Creatures;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroFactory
    {
        private EntityFactory _entityFactory;
        private AIFactory _aIFactory;
        private AbilityFactory _abilityFactory;
        private MainHeroHolderService _heroHolder;
        private ConfigsProviderService _configsProviderService;
        private StatsUpgradeService _statsUpgradeService;

        private readonly int _team = TeamTypes.MainHero;

        private EntitiesBuffer _entitiesBuffer;

        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aIFactory = container.Resolve<AIFactory>();
            _heroHolder = container.Resolve<MainHeroHolderService>();
            _abilityFactory = container.Resolve<AbilityFactory>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
            _statsUpgradeService = container.Resolve<StatsUpgradeService>();
        }

        public Entity Create(Vector3 position, MainHeroConfig config)
        {
            Entity entity = _entityFactory.CreateMainHero(position, GetStats(), config, _team);
            AIStateMachine brain = _aIFactory.CreateMainHeroBehaviour(entity, new NearestDamageableTargetSelector(entity.GetTransform(), entity.GetTeam()));

            entity
                .AddIsMainHero(new ReactiveVariable<bool>(true))
                .AddLevel(new ReactiveVariable<int>(1))
                .AddExperience()
                .AddCoins()
                .AddAbilityList();

            entity
                .AddBehaviour(new StateMachineBrainBehaviour(brain))
                .AddBehaviour(new LevelUpBehaviour(_configsProviderService.ExperienceForUpgradeLevelConfig))
                .AddBehaviour(new AbilityOnAddActivatorBehaviour());

            _heroHolder.Register(entity);
            _entitiesBuffer.Add(entity);

            return entity;
        }

        private Dictionary<StatTypes, float> GetStats()
        {
            Dictionary<StatTypes, float> stats = new();

            foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
                stats.Add(statType, _statsUpgradeService.GetCurrentStatValueFor(statType));

            return stats;
        }
    }
}
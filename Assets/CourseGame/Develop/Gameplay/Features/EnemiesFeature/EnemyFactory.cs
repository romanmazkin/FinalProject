using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Configs.Gameplay.Creatures;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature
{
    public class EnemyFactory
    {
        private EntityFactory _entityFactory;
        private AIFactory _aIFactory;

        private EntitiesBuffer _entitiesBuffer;

        private readonly int _team = TeamTypes.Enemies;

        public EnemyFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _aIFactory = container.Resolve<AIFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
        }

        public Entity Create(Vector3 position, CreatureConfig config)
        {
            Entity entity;
            AIStateMachine brain;

            switch (config)
            {
                case GhostConfig ghostConfig:
                    entity = _entityFactory.CreateGhost(position, ghostConfig, _team);
                    brain = _aIFactory.CreateGhostBehaviour(entity);
                    break;

                default:
                    throw new ArgumentException("Wrong enemy config.");
            }

            entity.AddBehaviour(new StateMachineBrainBehaviour(brain));

            _entitiesBuffer.Add(entity);

            return entity;
        }
    }
}

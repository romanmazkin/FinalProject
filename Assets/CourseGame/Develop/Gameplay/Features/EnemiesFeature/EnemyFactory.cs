using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
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

        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = _entityFactory.CreateGhost(position, _team);

            AIStateMachine brain = _aIFactory.CreateGhostBehaviour(entity);
            entity.AddBehaviour(new StateMachineBrainBehaviour(brain));

            _entitiesBuffer.Add(entity);

            return entity;
        }
    }
}

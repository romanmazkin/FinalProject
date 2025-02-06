using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroFactory
    {
        private EntityFactory _entityFactory;
        private AIFactory _aIFactory;

        private readonly int _team = TeamTypes.MainHero;

        private EntitiesBuffer _entitiesBuffer;

        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aIFactory = container.Resolve<AIFactory>();
        }

        public Entity Create(Vector3 position)
        {
            Entity entity = _entityFactory.CreateMainHero(position, _team);
            AIStateMachine brain = _aIFactory.CreateMainHeroBehaviour(entity);

            entity.AddBehaviour(new StateMachineBrainBehaviour(brain));
            _entitiesBuffer.Add(entity);

            return entity;
        }
    }
}

using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Configs.Gameplay.Creatures;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroFactory
    {
        private EntityFactory _entityFactory;
        private AIFactory _aIFactory;
        private MainHeroHolderService _heroHolder;

        private readonly int _team = TeamTypes.MainHero;

        private EntitiesBuffer _entitiesBuffer;

        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aIFactory = container.Resolve<AIFactory>();
            _heroHolder = container.Resolve<MainHeroHolderService>();
        }

        public Entity Create(Vector3 position, MainHeroConfig config)
        {
            Entity entity = _entityFactory.CreateMainHero(position, config, _team);
            AIStateMachine brain = _aIFactory.CreateMainHeroBehaviour(entity, new NearestDamageableTargetSelector(entity.GetTransform(), entity.GetTeam()));

            entity.AddIsMainHero(new ReactiveVariable<bool>(true));

            entity.AddBehaviour(new StateMachineBrainBehaviour(brain));

            _heroHolder.Register(entity);
            _entitiesBuffer.Add(entity);


            return entity;
        }
    }
}

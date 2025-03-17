using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class InitMainCharacterState : State, IUpdatableState
    {
        public MainHeroFactory _mainHeroFactory;
        public ConfigsProviderService _configsProviderService;

        public InitMainCharacterState(
            MainHeroFactory mainHeroFactory, 
            ConfigsProviderService configsProviderService)
        {
            _mainHeroFactory = mainHeroFactory;
            _configsProviderService = configsProviderService;
        }

        public ReactiveEvent MainCharacterSetupComplete { get; private set; } = new();

        public override void Enter()
        {
            base.Enter();

            Entity mainHero = _mainHeroFactory.Create(Vector3.zero, _configsProviderService.MainHeroConfig);

            MainCharacterSetupComplete?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}

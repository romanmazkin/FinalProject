using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.States;
using System.Collections;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        private GameplayStateMachine _gameplayStateMachine;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"asdfklhsdf {gameplayInputArgs.LevelNumber}");
            Debug.Log("sdfsdfsdf");
            Debug.Log("sdfsdfsdf");

            yield return new WaitForSeconds(1f);

            _gameplayStateMachine = CreateGameplayStateMachine();
            _gameplayStateMachine.Enter();
        }

        private void ProcessRegistrations()
        {
            _container.RegisterAsSingle<IInputService>(c => new DesktopInput());

            _container.RegisterAsSingle(c => new EntitiesBuffer());

            _container.RegisterAsSingle(c => new EntityFactory(c));
            _container.RegisterAsSingle(c => new AIFactory(c));
            _container.RegisterAsSingle(c => new EnemyFactory(c));

            _container.RegisterAsSingle(c => new MainHeroFactory(c));
            _container.RegisterAsSingle(c => new MainHeroHolderService());

            _container.RegisterAsSingle(c => new GameModesFactory(c));

            _container.RegisterAsSingle(c => new GameplayStateMachineDisposer());
            _container.RegisterAsSingle(c => new GameplayStateFactory(c));

            _container.Initialize();
        }

        private GameplayStateMachine CreateGameplayStateMachine()
        {
            GameplayStateMachineDisposer disposer = _container.Resolve<GameplayStateMachineDisposer>();

            GameplayStateFactory gameplayStateFactory = _container.Resolve<GameplayStateFactory>();

            InitMainCharacterState initMainCharacterState = gameplayStateFactory.CreateMainCharacterState();

            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();

            gameplayStateMachine.AddState(initMainCharacterState);

            disposer.Set(gameplayStateMachine);

            return gameplayStateMachine;
        }

        private void Update()
        {
            _gameplayStateMachine?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
            }
        }
    }
}

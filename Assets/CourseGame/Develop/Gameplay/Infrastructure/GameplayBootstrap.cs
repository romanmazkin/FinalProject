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
using Assets.CourseGame.Develop.Utils.Conditions;
using System;
using System.Collections;
using System.Collections.Generic;
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

            ProcessRegistrations(gameplayInputArgs);

            Debug.Log($"asdfklhsdf {gameplayInputArgs.LevelNumber}");
            Debug.Log("sdfsdfsdf");
            Debug.Log("sdfsdfsdf");

            yield return new WaitForSeconds(1f);

            _gameplayStateMachine = CreateGameplayStateMachine(gameplayInputArgs);
            _gameplayStateMachine.Enter();
        }

        private void ProcessRegistrations(GameplayInputArgs gameplayInputArgs)
        {
            _container.RegisterAsSingle<IInputService>(c => new DesktopInput());

            _container.RegisterAsSingle(c => new EntitiesBuffer());

            _container.RegisterAsSingle(c => new EntityFactory(c));
            _container.RegisterAsSingle(c => new AIFactory(c));
            _container.RegisterAsSingle(c => new EnemyFactory(c));

            _container.RegisterAsSingle(c => new MainHeroFactory(c));
            _container.RegisterAsSingle(c => new MainHeroHolderService());

            _container.RegisterAsSingle(c => new GameModesFactory(c));
            _container.RegisterAsSingle(c => new StageProviderService(
                c.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber)));

            _container.RegisterAsSingle(c => new GameplayStateMachineDisposer());
            _container.RegisterAsSingle(c => new GameplayStateFactory(c));



            _container.Initialize();
        }

        private GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs gameplayInputArgs)
        {
            GameplayStateMachineDisposer disposer = _container.Resolve<GameplayStateMachineDisposer>();

            GameplayStateFactory gameplayStateFactory = _container.Resolve<GameplayStateFactory>();

            InitMainCharacterState initMainCharacterState = gameplayStateFactory.CreateMainCharacterState();

            GameplayStateMachine gameLoopState = CreateGameLoopState(gameplayInputArgs);

            ActionCondition initMainCharacterToGameLoopStateCondition = new ActionCondition(initMainCharacterState.MainCharacterSetupComplete);

            List<IDisposable> disposables = new List<IDisposable>();
            disposables.Add(initMainCharacterToGameLoopStateCondition);
            disposables.Add(gameLoopState);

            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine(disposables);

            gameplayStateMachine.AddState(initMainCharacterState);
            gameplayStateMachine.AddState(gameLoopState);

            gameplayStateMachine.AddTransition(
                initMainCharacterState, gameLoopState,
                initMainCharacterToGameLoopStateCondition);

            disposer.Set(gameplayStateMachine);

            return gameplayStateMachine;
        }

        private GameplayStateMachine CreateGameLoopState(GameplayInputArgs gameplayInputArgs)
        {
            GameplayStateFactory gameplayStateFactory = _container.Resolve<GameplayStateFactory>();

            NextStagePreparationState nextStagePreparationState = gameplayStateFactory.CreateNextStagePreparationState();
            StageProcessState stageProcessState = gameplayStateFactory.CreateStageProcessState(gameplayInputArgs);

            ActionCondition preparationToStageProcessStateCondition = new ActionCondition(nextStagePreparationState.OnNextStageTriggerComplete);

            ActionCondition stageProcessToPreparationStateCondition = new ActionCondition(stageProcessState.StageComplete);

            List<IDisposable> disposables = new List<IDisposable>();
            disposables.Add(preparationToStageProcessStateCondition);
            disposables.Add(stageProcessToPreparationStateCondition);

            GameplayStateMachine gameplayLoopState = new GameplayStateMachine(disposables);

            gameplayLoopState.AddState(nextStagePreparationState);
            gameplayLoopState.AddState(stageProcessState);

            gameplayLoopState.AddTransition(
                nextStagePreparationState, stageProcessState,
                preparationToStageProcessStateCondition);

            gameplayLoopState.AddTransition(
                stageProcessState, nextStagePreparationState,
                stageProcessToPreparationStateCondition);

            return gameplayLoopState;
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

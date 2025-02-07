using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
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
            _container.RegisterAsSingle<IPauseService>(c => new TimeScalePauseService());

            _container.RegisterAsSingle(c => new EntitiesBuffer());

            _container.RegisterAsSingle(c => new EntityFactory(c));
            _container.RegisterAsSingle(c => new AIFactory(c));
            _container.RegisterAsSingle(c => new EnemyFactory(c));

            _container.RegisterAsSingle(c => new MainHeroFactory(c));
            _container.RegisterAsSingle(c => new MainHeroHolderService());
            _container.RegisterAsSingle(c => new MainHeroFinishConditionCreator(
                c.Resolve<MainHeroHolderService>(), c.Resolve<GameplayFinishConditionService>())).NonLazy();


            _container.RegisterAsSingle(c => new GameModesFactory(c));
            _container.RegisterAsSingle(c => new StageProviderService(
                c.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber)));

            _container.RegisterAsSingle(c => new GameplayStateMachineDisposer());
            _container.RegisterAsSingle(c => new GameplayStateFactory(c));
            _container.RegisterAsSingle(c => new GameplayFinishConditionService());



            _container.Initialize();
        }

        private GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs gameplayInputArgs)
        {
            GameplayStateMachineDisposer disposer = _container.Resolve<GameplayStateMachineDisposer>();
            GameplayFinishConditionService gameplayFinishConditionService = _container.Resolve<GameplayFinishConditionService>();

            GameplayStateFactory gameplayStateFactory = _container.Resolve<GameplayStateFactory>();

            InitMainCharacterState initMainCharacterState = gameplayStateFactory.CreateMainCharacterState();
            GameplayStateMachine gameLoopState = gameplayStateFactory.CreateGameLoopState(gameplayInputArgs);
            DefeatState defeatState = gameplayStateFactory.CreateDefeatState();
            WinState winState = gameplayStateFactory.CreateWinState(gameplayInputArgs);

            ActionCondition initMainCharacterToGameLoopStateCondition = new ActionCondition(initMainCharacterState.MainCharacterSetupComplete);

            List<IDisposable> disposables = new List<IDisposable>();
            disposables.Add(initMainCharacterToGameLoopStateCondition);
            disposables.Add(gameLoopState);

            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine(disposables);

            gameplayStateMachine.AddState(initMainCharacterState);
            gameplayStateMachine.AddState(gameLoopState);
            gameplayStateMachine.AddState(defeatState);
            gameplayStateMachine.AddState(winState);

            gameplayStateMachine.AddTransition(
                initMainCharacterState, gameLoopState,
                initMainCharacterToGameLoopStateCondition);

            gameplayStateMachine.AddTransition(
                gameLoopState, winState,
                gameplayFinishConditionService.WinCondition);

            gameplayStateMachine.AddTransition(
                gameLoopState, defeatState,
                gameplayFinishConditionService.DefeatCondition);

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

            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach(Entity entity in _container.Resolve<EntitiesBuffer>().Elements)
                {
                    if(entity.TryGetTeam(out var team) && team.Value == TeamTypes.Enemies)
                    {
                        if (entity.TryGetTakeDamageRequest(out var takeDamageRequest))
                            takeDamageRequest.Invoke(99999);
                    }
                }
            }
        }
    }
}

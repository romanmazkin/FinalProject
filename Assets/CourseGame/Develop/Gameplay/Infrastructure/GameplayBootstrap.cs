using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.CommonUI.Wallet;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.AbilityDropServiceFeature;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Presenters;
using Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature;
using Assets.CourseGame.Develop.Gameplay.Features.LootFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using Assets.CourseGame.Develop.Gameplay.States;
using Assets.CourseGame.Develop.Gameplay.UI;
using Assets.CourseGame.Develop.Gameplay.UI.HealthBars;
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
        private CreaturesHealthDisplayPresenter _creatureHealthDisplayPresenter;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations(gameplayInputArgs);

            _creatureHealthDisplayPresenter = _container.Resolve<CreaturesHealthDisplayPresenter>();

            Debug.Log($"Load level {gameplayInputArgs.LevelNumber}");

            yield return new WaitForSeconds(1f);

            _gameplayStateMachine = CreateGameplayStateMachine(gameplayInputArgs);
            _gameplayStateMachine.Enter();
        }

        private void ProcessRegistrations(GameplayInputArgs gameplayInputArgs)
        {
            _container.RegisterAsSingle<IInputService>(c => new DesktopInput(_container.Resolve<GameplayUIRoot>().Joystick));
            _container.RegisterAsSingle<IPauseService>(c => new TimeScalePauseService());

            _container.RegisterAsSingle(c => new EntitiesBuffer());

            _container.RegisterAsSingle(c => new EntityFactory(c));
            _container.RegisterAsSingle(c => new AIFactory(c));
            _container.RegisterAsSingle(c => new EnemyFactory(c));

            _container.RegisterAsSingle(c => new HealthBarFactory(c));
            _container.RegisterAsSingle(c => new CreaturesHealthPresentersFactory(c));

            _container.RegisterAsSingle(c => new NextStagePreperationFrameFactory(c));

            _container.RegisterAsSingle(c =>
            c.Resolve<CreaturesHealthPresentersFactory>().CreateHealthDisplayPresenter()).NonLazy();

            _container.RegisterAsSingle(c => new GameplayUIFactory(c));

            _container.RegisterAsSingle(c => new MainHeroCoinsViewCreatorService(
            c.Resolve<MainHeroHolderService>(), c.Resolve<WalletPresenterFactory>(),
            c.Resolve<GameplayUIRoot>())).NonLazy();

            _container.RegisterAsSingle(c => new MainHeroExperienceBarCreatorService(
            c.Resolve<MainHeroHolderService>(), c.Resolve<GameplayUIFactory>())).NonLazy();

            _container.RegisterAsSingle(c => _container.Resolve<GameplayUIFactory>().CreateStagePresenter()).NonLazy();

            _container.RegisterAsSingle(c => new LootFactory(c));
            _container.RegisterAsSingle(c => new DropLootService(
                c.Resolve<ConfigsProviderService>(), c.Resolve<LootFactory>()));
            _container.RegisterAsSingle(c => new LootPullingService(c.Resolve<EntitiesBuffer>())).NonLazy();

            _container.RegisterAsSingle(c => new AbilityFactory(c));
            _container.RegisterAsSingle(c => new AbilityPresentersFactory(c));
            _container.RegisterAsSingle(c => new DropAbilityOnMainHeroLevelUpService(
                c.Resolve<MainHeroHolderService>(), c.Resolve<AbilityPresentersFactory>(),
                c.Resolve<IPauseService>(), c.Resolve<ICoroutinePerformer>())).NonLazy();
            _container.RegisterAsSingle(c => new AbilityDropService(
                c.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber).AbilityDropOptionsConfig,
                new AbilityDropingRules()));

            _container.RegisterAsSingle(c => new MainHeroFactory(c));
            _container.RegisterAsSingle(c => new MainHeroHolderService());
            _container.RegisterAsSingle(c => new MainHeroFinishConditionCreator(
                c.Resolve<MainHeroHolderService>(), c.Resolve<GameplayFinishConditionService>())).NonLazy();


            _container.RegisterAsSingle(c => new GameModesFactory(c));
            _container.RegisterAsSingle(c => new StageProviderService(
                c.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber)));

            _container.RegisterAsSingle(c => new GameplayStateMachineDisposer());
            _container.RegisterAsSingle(c => new GameplayStatesFactory(c));
            _container.RegisterAsSingle(c => new GameplayFinishConditionService());

            _container.RegisterAsSingle(c =>
            {
                GameplayUIRoot gameplayUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<GameplayUIRoot>("Gameplay/UI/GameplayUIRoot");
                return Instantiate(gameplayUIRootPrefab);
            }).NonLazy();

            _container.Initialize();
        }

        private GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs gameplayInputArgs)
        {
            GameplayStateMachineDisposer disposer = _container.Resolve<GameplayStateMachineDisposer>();
            GameplayFinishConditionService gameplayFinishConditionService = _container.Resolve<GameplayFinishConditionService>();

            GameplayStatesFactory gameplayStateFactory = _container.Resolve<GameplayStatesFactory>();

            InitMainCharacterState initMainCharacterState = gameplayStateFactory.CreateInitMainCharacterState();
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

        private AbilitySelectPopupPresenter _popup;

        private void Update()
        {
            _gameplayStateMachine?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach (Entity entity in _container.Resolve<EntitiesBuffer>().Elements)
                {
                    if (entity.TryGetTeam(out var team) && team.Value == TeamTypes.Enemies)
                    {
                        if (entity.TryGetTakeDamageRequest(out var takeDamageRequest))
                            takeDamageRequest.Invoke(99999);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _container.Resolve<MainHeroHolderService>().MainHero.GetExperience().Value += 150;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                _container.Resolve<IPauseService>().Pause();
                _popup = _container.Resolve<AbilityPresentersFactory>().CreateAbilitySelectPopupPresenter(_container.Resolve<MainHeroHolderService>().MainHero);
                _popup.Enable();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                _popup.Disable(() => _container.Resolve<IPauseService>().Unpause());
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                _container.Resolve<MainHeroHolderService>().MainHero.GetInstantAttackEvent().Invoke();
            }
        }

        private void LateUpdate()
        {
            _creatureHealthDisplayPresenter.LateUpdate();
        }
    }
}
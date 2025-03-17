using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.DataManagement.DataProviders;
using Assets.CourseGame.Develop.CommonServices.LevelsManagement;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.LootFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;
using Assets.CourseGame.Develop.Utils.Conditions;
using System;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private DIContainer _container;

        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public InitMainCharacterState CreateInitMainCharacterState()
        {
            return new InitMainCharacterState(_container.Resolve<MainHeroFactory>(), _container.Resolve<ConfigsProviderService>());
        }

        public NextStagePreperationState CreateNextStagePreperationState()
        {
            return new NextStagePreperationState(
                _container.Resolve<EntityFactory>(),
                _container.Resolve<StageProviderService>(),
                _container.Resolve<NextStagePreperationFrameFactory>());
        }

        public StageProcessState CreateStageProcessState(GameplayInputArgs gameplayInputArgs)
        {
            return new StageProcessState(
                _container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber),
                _container.Resolve<GameModesFactory>(),
                _container.Resolve<StageProviderService>());
        }

        public WinState CreateWinState(GameplayInputArgs gameplayInputArgs)
        {
            return new WinState(
                _container.Resolve<CompletedLevelsService>(),
                _container.Resolve<PlayerDataProvider>(),
                gameplayInputArgs,
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<IPauseService>(),
                _container.Resolve<IInputService>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<MainHeroHolderService>());
        }

        public DefeatState CreateDefeatState()
        {
            return new DefeatState(
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<IPauseService>(),
                _container.Resolve<IInputService>());
        }

        public CollectLootState CreateCollectLootState()
        {
            return new CollectLootState(
                _container.Resolve<LootPullingService>(),
                _container.Resolve<MainHeroHolderService>());
        }

        public GameplayStateMachine CreateGameLoopState(GameplayInputArgs gameplayInputArgs)
        {
            GameplayStatesFactory gameplayStatesFactory = _container.Resolve<GameplayStatesFactory>();
            GameplayFinishConditionService gameplayFinishConditionService = _container.Resolve<GameplayFinishConditionService>();
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();

            NextStagePreperationState nextStagePreperationState = gameplayStatesFactory.CreateNextStagePreperationState();
            StageProcessState stageProcessState = gameplayStatesFactory.CreateStageProcessState(gameplayInputArgs);
            CollectLootState collectLootState = gameplayStatesFactory.CreateCollectLootState();

            ActionCondition preperationToStageProcesStateCondition = new ActionCondition(nextStagePreperationState.OnNextStageTriggerComplete);

            ActionCondition stageProcessToCollectStateCondition = new ActionCondition(stageProcessState.StageComplete);
            ActionCondition collectToPreperationStateCondition = new ActionCondition(collectLootState.LootCollected);

            gameplayFinishConditionService.WinCondition
                .Add(new ActionCondition(nextStagePreperationState.OnNextStageTriggerComplete))
                .Add(new FuncCondition(() => stageProviderService.StageResult == StageResult.Completed
                && stageProviderService.HasNextStage() == false));

            List<IDisposable> disposables = new List<IDisposable>();
            disposables.Add(preperationToStageProcesStateCondition);
            disposables.Add(stageProcessToCollectStateCondition);
            disposables.Add(collectToPreperationStateCondition);

            GameplayStateMachine gameplayLoopState = new GameplayStateMachine(disposables);

            gameplayLoopState.AddState(nextStagePreperationState);
            gameplayLoopState.AddState(collectLootState);
            gameplayLoopState.AddState(stageProcessState);

            gameplayLoopState.AddTransition(
                nextStagePreperationState, stageProcessState,
                preperationToStageProcesStateCondition);

            gameplayLoopState.AddTransition(
                stageProcessState, collectLootState,
                stageProcessToCollectStateCondition);

            gameplayLoopState.AddTransition(
                collectLootState, nextStagePreperationState,
                collectToPreperationStateCondition);

            return gameplayLoopState;
        }
    }
}

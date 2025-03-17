using Assets.CourseGame.Develop.Configs.Gameplay;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature.WaveGameModeFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class StageProcessState : State, IUpdatableState
    {
        private LevelConfig _levelConfig;
        private GameModesFactory _gameModesFactory;
        private StageProviderService _stageProviderService;

        private WaveGameMode _gameMode;

        private IDisposable _gameModeEndedDisposable;

        public StageProcessState(
            LevelConfig levelConfig,
            GameModesFactory gameModesFactory,
            StageProviderService stageProviderService)
        {
            _levelConfig = levelConfig;
            _gameModesFactory = gameModesFactory;
            _stageProviderService = stageProviderService;
        }

        public ReactiveEvent StageComplete { get; } = new();

        public override void Enter()
        {
            base.Enter();

            _gameMode = _gameModesFactory.CreateWaveGameMode();

            _gameMode.Start(_levelConfig.WaveConfigs[_stageProviderService.NextStageIndex.Value]);
            _gameModeEndedDisposable = _gameMode.Ended.Subscribe(OnGameModeEnded);
        }

        private void OnGameModeEnded()
        {
            _stageProviderService.CompleteStage();

            if (_stageProviderService.HasNextStage())
                _stageProviderService.SwitchToNext();

            StageComplete?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            _gameModeEndedDisposable?.Dispose();
            _gameMode.Cleanup();
            _gameMode = null;
        }

        public void Update(float deltaTime)
        {
        }
    }
}
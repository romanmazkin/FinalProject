using Assets.CourseGame.Develop.Configs.Gameplay;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature
{
    public class StageProviderService
    {
        private ReactiveVariable<int> _nextStageIndex = new();
        private LevelConfig _levelConfig;

        public StageProviderService(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public IReadOnlyVariable<int> NextStageIndex => _nextStageIndex;

        public StageResult StageResult {  get; private set; } = StageResult.Uncompleted;

        public int StageCount => _levelConfig.WaveConfigs.Count;

        public bool HasNextStage() => _nextStageIndex.Value < StageCount - 1;

        public void CompleteStage() => StageResult = StageResult.Completed;

        public void SwitchToNext()
        {
            if (HasNextStage() == false)
                throw new InvalidOperationException();

            _nextStageIndex.Value++;
        }
    }
}

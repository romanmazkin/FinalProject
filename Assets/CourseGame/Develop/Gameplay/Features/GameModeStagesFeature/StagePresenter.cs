using Assets.CourseGame.Develop.CommonUI;
using Assets.CourseGame.Develop.DI;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature
{
    public class StagePresenter : IDisposable, IInitializeable
    {
        private IconWithText _view;
        private StageProviderService _stageProviderService;

        public StagePresenter(IconWithText view, StageProviderService stageProviderService)
        {
            _view = view;
            _stageProviderService = stageProviderService;
        }

        public void Dispose() => Disable();

        public void Initialize() => Enable();

        public void Enable()
        {
            _stageProviderService.NextStageIndex.Changed += OnNextStageIndexChanged;

            UpdateStageNumber();
        }

        public void Disable()
        {
            _stageProviderService.NextStageIndex.Changed -= OnNextStageIndexChanged;
        }

        private void OnNextStageIndexChanged(int arg1, int arg2) => UpdateStageNumber();

        private void UpdateStageNumber()
        {
            _view.SetText($"{_stageProviderService.NextStageIndex.Value + 1} / {_stageProviderService.StageCount}");
        }
    }
}
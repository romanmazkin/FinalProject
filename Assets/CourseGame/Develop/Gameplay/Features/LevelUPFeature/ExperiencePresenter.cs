using Assets.CourseGame.Develop.CommonUI;
using Assets.CourseGame.Develop.Configs.Gameplay;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature
{
    public class ExperiencePresenter
    {
        private BarWithText _view;

        private ExperienceForUpgradeLevelConfig _levelUpConfig;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _currentLevel;

        public ExperiencePresenter(
            Entity entity, 
            BarWithText view, 
            ExperienceForUpgradeLevelConfig levelUpConfig)
        {
            _view = view;
            _levelUpConfig = levelUpConfig;
            _experience = entity.GetExperience();
            _currentLevel = entity.GetLevel();
        }

        public void Enable()
        {
            _experience.Changed += OnCurrentExperienceChanged;
            _currentLevel.Changed += OnLevelChanged;

            UpdateBarText(_currentLevel.Value);
            UpdateCurrentExperience(_experience.Value);
        }

        public void Disable()
        {
            _experience.Changed -= OnCurrentExperienceChanged;
            _currentLevel.Changed -= OnLevelChanged;

            UnityEngine.Object.Destroy(_view.gameObject);
        }

        private void UpdateCurrentExperience(float value)
            => _view.UpdateSlider(value / _levelUpConfig.GetExperienceFor(_currentLevel.Value));

        private void UpdateBarText(int level) => _view.UpdateText($"Lv.{level}");

        private void OnLevelChanged(int arg1, int arg2) 
            => UpdateBarText(_currentLevel.Value);

        private void OnCurrentExperienceChanged(float arg1, float arg2)
            => UpdateCurrentExperience(_experience.Value);
    }
}

using Assets.CourseGame.Develop.Configs.Gameplay;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature
{
    public class LevelUpBehaviour : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _level;
        private ExperienceForUpgradeLevelConfig _config;

        public LevelUpBehaviour(ExperienceForUpgradeLevelConfig config)
        {
            _config = config;
        }

        public float CurrentLimitForExp => _config.GetExperienceFor(_level.Value);

        public void OnInit(Entity entity)
        {
            _experience = entity.GetExperience();
            _level = entity.GetLevel();

            _experience.Changed += OnExperienceChanged;
        }

        private void OnExperienceChanged(float arg1, float newExp)
        {
            if (_level.Value >= _config.MaxLevel)
                return;

            while(newExp >= CurrentLimitForExp)
            {
                newExp -= CurrentLimitForExp;
                _level.Value++;
            }

            _experience.Value = newExp;
        }

        public void OnDispose()
        {
            _experience.Changed -= OnExperienceChanged;
        }
    }
}

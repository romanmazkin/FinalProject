using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature
{
    public abstract class Ability
    {
        private ReactiveVariable<int> _currentLevel;

        protected Ability(string id, int currentLevel, int maxLevel)
        {
            if (currentLevel > maxLevel)
                throw new ArgumentException(nameof(currentLevel));

            ID = id;
            MaxLevel = maxLevel;
            _currentLevel = new ReactiveVariable<int>(currentLevel);
        }

        public string ID { get; }
        public int MaxLevel { get; }
        public IReadOnlyVariable<int> CurrentLevel => _currentLevel;

        public void AddLevel(int level)
        {
            int temp = _currentLevel.Value + level;

            if(temp > MaxLevel)
                throw new ArgumentException(nameof(level));

            _currentLevel.Value = temp;
        }

        public abstract void Activate();
    }
}

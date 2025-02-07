using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelListConfig", fileName = "LevelListConfig")]

    public class LevelListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetBy(int level)
        {
            int levelIndex = level - 1;

            if (levelIndex >= _levels.Count)
                throw new ArgumentException(nameof(level));

            return _levels[levelIndex];
        }
    }
}

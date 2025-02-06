using Assets.CourseGame.Develop.Configs.Gameplay.Levels.WaveStage;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]

    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> _waveConfigs;

        public IReadOnlyList<WaveConfig> WaveConfigs => _waveConfigs;
    } 
}

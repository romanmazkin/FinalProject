using Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions;
using Assets.CourseGame.Develop.Configs.Gameplay.Levels.WaveStage;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]

    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> _waveConfigs;
        [field: SerializeField] public AbilityDropOptionsConfig AbilityDropOptionsConfig {  get; private set; }

        public IReadOnlyList<WaveConfig> WaveConfigs => _waveConfigs;
    } 
}

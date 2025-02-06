using Assets.CourseGame.Develop.Configs.Gameplay.Creatures;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Levels.WaveStage
{
    [Serializable]
    public class WaveItemConfig
    {
        [field: SerializeField] public Vector3 SpawnPosition {  get; private set; }
        [field: SerializeField] public CreatureConfig EnemyConfig {  get; private set; }
    }
}

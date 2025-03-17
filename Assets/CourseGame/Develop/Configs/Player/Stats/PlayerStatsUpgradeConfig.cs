using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Player.Stats
{
    [CreateAssetMenu(menuName = "Configs/Player/PlayerStatsUpgradeConfig", fileName = "PlayerStatsUpgradeConfig")]
    public class PlayerStatsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<StatUpgradeCostConfig> _stats = new List<StatUpgradeCostConfig>();

        public StatUpgradeCostConfig GetStatConfig(StatTypes type)
            => _stats.First(s => s.Type == type);
    }

    [Serializable]
    public class StatUpgradeCostConfig
    {
        [field: SerializeField] public StatTypes Type { get; private set; }
        [field: SerializeField] public List<float> StatValues { get; private set; }
        [field: SerializeField] public CurrencyTypes CostType { get; private set; } = CurrencyTypes.Gold;
        [field: SerializeField] public List<int> UpgradeToNextLevelCost { get; private set; }
    }
}

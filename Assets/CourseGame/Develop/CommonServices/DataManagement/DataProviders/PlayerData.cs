using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;
using System;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.CommonServices.DataManagement.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
        public List<int> CompletedLevels;
        public Dictionary<StatTypes, int> StatsUpgradeLevel;
    }
}

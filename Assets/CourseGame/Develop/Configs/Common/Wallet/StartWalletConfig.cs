using Assets.CourseGame.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Common.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/NewStartWallerConfig", fileName = "StartWallerConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        private void OnValidate()
        {
            // check  
        }

        public int GetStartValueFor(CurrencyTypes currencyType) => _values.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}

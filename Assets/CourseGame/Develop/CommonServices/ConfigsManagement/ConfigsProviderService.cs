using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.Configs.Common.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CourseGame.Develop.CommonServices.ConfigsManagement
{
    public class ConfigsProviderService
    {
        private ResourcesAssetLoader _resourcesAssetLoader;

        public ConfigsProviderService(ResourcesAssetLoader resourcesAssetLoader)
        {
            _resourcesAssetLoader = resourcesAssetLoader;
        }

        public StartWalletConfig StartWalletConfig { get; private set; }

        public CurrencyIconsConfig CurrencyIconsConfig { get; private set; }

        public void LoadAll()
        {
            //Load configs
            LoadStartWalletConfig();
            LoadCurrencyIconsConfig();
        }

        public void LoadStartWalletConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWallerConfig");

        public void LoadCurrencyIconsConfig()
            => CurrencyIconsConfig = _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");
    }
}

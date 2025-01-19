using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.Configs.Common.Wallet;
using Assets.CourseGame.Develop.Configs.Gameplay;

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

        public LevelListConfig LevelsListConfig { get; private set; }

        public void LoadAll()
        {
            //Load configs
            LoadStartWalletConfig();
            LoadCurrencyIconsConfig();
            LoadLevelsListConfig();
        }

        public void LoadStartWalletConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWallerConfig");

        public void LoadCurrencyIconsConfig()
            => CurrencyIconsConfig = _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");

        public void LoadLevelsListConfig()
            => LevelsListConfig = _resourcesAssetLoader.LoadResource<LevelListConfig>("Configs/Gameplay/Levels/LevelListConfig");
    }
}

using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.CommonUI.Wallet;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;
using UnityEngine;

namespace Assets.CourseGame.Develop.MainMenu.StatsUpgradeFeature
{
    public class StatsUpgradePopupFactory
    {
        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _resourcesAssetLoader;
        private readonly MainMenuUIRoot _mainMenuUIRoot;
        private readonly ConfigsProviderService _configsProviderService;

        public StatsUpgradePopupFactory(DIContainer container)
        {
            _container = container;
            _mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            _resourcesAssetLoader = _container.Resolve<ResourcesAssetLoader>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();
        }

        public UpgradableStatPresenter CreateUpgradableStatPresenter(UpgradableStatView view, StatTypes statType)
        {
            return new UpgradableStatPresenter(
                view,
                statType,
                _configsProviderService.StatsViewConfig,
                _container.Resolve<StatsUpgradeService>(),
                _container.Resolve<WalletService>(),
                _configsProviderService.CurrencyIconsConfig);
        }

        public StatsUpgradePopupPresenter CreatePopup()
        {
            StatsUpgradePopupView statsUpgradePopupViewPrefab = _resourcesAssetLoader.LoadResource<StatsUpgradePopupView>("MainMenu/UI/StatsUpgradePopup/StatsUpgradePopupView");
            StatsUpgradePopupView statsUpgradePopupView = Object.Instantiate(statsUpgradePopupViewPrefab, _mainMenuUIRoot.PopupsLayer);

            return new StatsUpgradePopupPresenter(
                statsUpgradePopupView,
                _container.Resolve<StatsUpgradePopupFactory>(),
                _container.Resolve<WalletPresenterFactory>(),
                _container.Resolve<StatsUpgradeService>());
        }

        public CharacterPreviewPresenter CreateCharacterPreviewPresenter()
        {
            return new CharacterPreviewPresenter(_container.Resolve<ISceneLoader>(), _container.Resolve<ICoroutinePerformer>());
        }
    }
}
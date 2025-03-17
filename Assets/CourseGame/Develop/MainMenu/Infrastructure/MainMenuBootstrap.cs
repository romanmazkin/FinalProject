using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.DataManagement.DataProviders;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.CommonUI.Wallet;
using Assets.CourseGame.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup;
using Assets.CourseGame.Develop.MainMenu.StatsUpgradeFeature;
using System.Collections;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        InitializeUI();

        yield return new WaitForSeconds(1f);
    }

    private void InitializeUI()
    {
        MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
        mainMenuUIRoot.OpenLevelsMenuButton.Initialize(() =>
        {
            LevelsMenuPopupPresenter levelsMenuPopupPresenter = _container.Resolve<LevelsMenuPopupFactory>().CreateLevelsMenuPopupPresenter();
            levelsMenuPopupPresenter.Enable();
        });

        mainMenuUIRoot.OpenStatsUpgradePopupButton.Initialize(() =>
        {
            StatsUpgradePopupPresenter statsUpgradePopupPresenter = _container.Resolve<StatsUpgradePopupFactory>().CreatePopup();
            statsUpgradePopupPresenter.Enable();
        });
    }

    private void ProcessRegistrations()
    {
        //registrations foe this scene
        _container.RegisterAsSingle(c => new LevelsMenuPopupFactory(c));
        _container.RegisterAsSingle(c => new StatsUpgradePopupFactory(c));

        _container.RegisterAsSingle(c =>
        {
            MainMenuUIRoot mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");
            return Instantiate(mainMenuUIRootPrefab);
        }).NonLazy();

        _container
            .RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
            .CreateWalletPresenter(c.Resolve<MainMenuUIRoot>().WalletView))
            .NonLazy();

        _container.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            WalletService wallet = _container.Resolve<WalletService>();
            wallet.Add(CurrencyTypes.Gold, 100);
            Debug.Log("Money is " + wallet.GetCurrency(CurrencyTypes.Gold).Value);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _container.Resolve<PlayerDataProvider>().Save();
        }
    }
}

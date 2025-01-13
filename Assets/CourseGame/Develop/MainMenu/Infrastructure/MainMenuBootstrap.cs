using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.DataManagement.DataProviders;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.CommonUI.Wallet;
using System.Collections;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        yield return new WaitForSeconds(1f);
    }

    private void ProcessRegistrations()
    {
        //registrations foe this scene

        _container.RegisterAsSingle(c => new WalletPresenterFactory(c));

        _container.RegisterAsSingle(c =>
        {
            MainMenuUIRoot mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");
            return Instantiate(mainMenuUIRootPrefab);
        }).NonLazy();

        _container.RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
        .CreateCurrencyPresenter(c.Resolve<MainMenuUIRoot>()._currencyView, CurrencyTypes.Gold))
            .NonLazy();

        _container.Initialize();
    }

    private CurrencyPresenter _currencyPresenter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _currencyPresenter?.Dispose();
            MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            _currencyPresenter = _container.Resolve<WalletPresenterFactory>().CreateCurrencyPresenter(mainMenuUIRoot._currencyView, CurrencyTypes.Gold);
            _currencyPresenter.Initialize();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _currencyPresenter?.Dispose();
            MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            _currencyPresenter = _container.Resolve<WalletPresenterFactory>().CreateCurrencyPresenter(mainMenuUIRoot._currencyView, CurrencyTypes.Diamond);
            _currencyPresenter.Initialize();
        }

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

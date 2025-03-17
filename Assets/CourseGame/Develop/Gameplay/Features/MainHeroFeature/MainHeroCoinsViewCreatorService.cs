using Assets.CourseGame.Develop.CommonUI.Wallet;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.UI;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroCoinsViewCreatorService : IDisposable
    {
        private MainHeroHolderService _mainHeroHolderService;
        private WalletPresenterFactory _walletPresentersFactory;
        private CurrencyPresenter _currencyPresenter;
        private CoinsAddedEffectPresenter _coinsAddedEffectPresenter;
        private GameplayUIRoot _gameplayUIRoot;

        private IDisposable _heroRegistredDisposable;
        private IDisposable _heroUnregistredDisposable;

        public MainHeroCoinsViewCreatorService(
            MainHeroHolderService mainHeroHolderService,
            WalletPresenterFactory walletPresentersFactory,
            GameplayUIRoot gameplayUIRoot)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _heroRegistredDisposable = _mainHeroHolderService.HeroRegistred.Subscribe(OnMainHeroRegister);
            _heroUnregistredDisposable = _mainHeroHolderService.HeroUnregistred.Subscribe(OnMainHeroUnregister);
            _walletPresentersFactory = walletPresentersFactory;
            _gameplayUIRoot = gameplayUIRoot;
        }

        private void OnMainHeroUnregister(Entity hero)
        {
            _currencyPresenter.Dispose();
            _currencyPresenter = null;

            _coinsAddedEffectPresenter.Dispose();
            _coinsAddedEffectPresenter = null;
        }

        private void OnMainHeroRegister(Entity hero)
        {
            _currencyPresenter = _walletPresentersFactory.CreateCurrencyPresenter(
                _gameplayUIRoot.CoinsView, CommonServices.Wallet.CurrencyTypes.Gold, hero.GetCoins());
            _currencyPresenter.Initialize();

            _coinsAddedEffectPresenter = new CoinsAddedEffectPresenter(
                _gameplayUIRoot.CoinsAddedEffectView, hero.GetCoins(), hero.GetTransform());
            _coinsAddedEffectPresenter.Initialize();
        }

        public void Dispose()
        {
            _heroRegistredDisposable.Dispose();
            _heroUnregistredDisposable.Dispose();
        }
    }
}
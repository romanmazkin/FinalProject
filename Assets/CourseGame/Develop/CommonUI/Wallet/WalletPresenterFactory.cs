using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.CommonUI.Wallet
{
    public class WalletPresenterFactory
    {
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public WalletPresenterFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        public WalletPresenter CreateWalletPresenter(IconWithTextListView view)
            => new WalletPresenter(_walletService, view, this);

        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresenter(_walletService.GetCurrency(currencyType), currencyType, view, _configsProviderService.CurrencyIconsConfig);

        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType, IReadOnlyVariable<int> currency)
            => new CurrencyPresenter(currency, currencyType, view, _configsProviderService.CurrencyIconsConfig);
    }
}

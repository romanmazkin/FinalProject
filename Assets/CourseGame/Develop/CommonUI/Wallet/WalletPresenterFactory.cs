using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.Wallet;

namespace Assets.CourseGame.Develop.CommonUI.Wallet
{
    public class WalletPresenterFactory
    {
        private WalletService _walletService;
        private ConfigsProviderService _configProviderService;

        public WalletPresenterFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configProviderService = container.Resolve<ConfigsProviderService>();
        }

        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresenter(_walletService.GetCurrency(currencyType), currencyType, view, _configProviderService.CurrencyIconsConfig);
    }
}

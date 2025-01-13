using Assets.CourseGame.Develop.CommonServices.Wallet;
using Assets.CourseGame.Develop.Configs.Common.Wallet;
using Assets.CourseGame.Develop.DI;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.CommonUI.Wallet
{
    public class CurrencyPresenter : IInitializeable, IDisposable
    {
        private IReadOnlyVariable<int> _currency;
        private CurrencyTypes _currencyType;
        private CurrencyIconsConfig _currencyIconsConfig;

        private IconWithText _currencyView;

        public CurrencyPresenter(
            IReadOnlyVariable<int> currency, 
            CurrencyTypes currencyType, 
            IconWithText currencyView,
            CurrencyIconsConfig currencyIconsConfig)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyView = currencyView;
            _currencyIconsConfig = currencyIconsConfig;
        }

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _currencyView.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _currency.Changed += OnCurrencyChanged;
        }

        public void Dispose()
        {
            _currency.Changed -= OnCurrencyChanged;
        }

        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _currencyView.SetText(value.ToString());
    }
}

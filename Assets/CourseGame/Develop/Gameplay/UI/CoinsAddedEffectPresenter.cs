using Assets.CourseGame.Develop.DI;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI
{
    public class CoinsAddedEffectPresenter : IInitializeable, IDisposable
    {
        private CoinsAddedEffectView _view;
        private ReactiveVariable<int> _coins;
        private Transform _source;

        public CoinsAddedEffectPresenter(
            CoinsAddedEffectView view,
            ReactiveVariable<int> coins,
            Transform source)
        {
            _view = view;
            _coins = coins;
            _source = source;
        }

        public void Initialize()
        {
            _coins.Changed += OnCoinsChanged;
        }

        private void OnCoinsChanged(int oldValue, int newValue)
        {
            if (newValue > oldValue)
                _view.SpawnEffect(_source.position);
        }

        public void Dispose()
        {
            _coins.Changed -= OnCoinsChanged;
        }
    }
}

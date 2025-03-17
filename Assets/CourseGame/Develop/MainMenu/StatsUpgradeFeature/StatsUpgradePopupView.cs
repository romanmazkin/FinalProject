using Assets.CourseGame.Develop.CommonUI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CourseGame.Develop.MainMenu.StatsUpgradeFeature
{
    public class StatsUpgradePopupView : MonoBehaviour
    {
        public event Action CloseRequest;

        [SerializeField] private Transform _upgradableStatsParent;
        [SerializeField] private UpgradableStatView _upgradableStatViewPrefab;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _closeButton;

        [field: SerializeField] public IconWithTextListView CurrencyListView { get; private set; }

        private List<UpgradableStatView> _upgradableStatViews = new();

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void SetTitle(string title) => _title.text = title;

        public UpgradableStatView SpawnElement()
        {
            UpgradableStatView upgradableStatView = Instantiate(_upgradableStatViewPrefab, _upgradableStatsParent);
            _upgradableStatViews.Add(upgradableStatView);
            return upgradableStatView;
        }

        public void Remove(UpgradableStatView upgradableStatView)
        {
            _upgradableStatViews.Remove(upgradableStatView);
            Destroy(upgradableStatView.gameObject);
        }

        private void OnCloseButtonClicked()
        {
            CloseRequest?.Invoke();
        }
    }
}
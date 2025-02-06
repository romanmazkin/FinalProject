using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CourseGame.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelsMenuPopupView : MonoBehaviour
    {
        public event Action CloseRequest;

        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private LevelTileListView _levelTileListView;

        public LevelTileListView LevelTileListView => _levelTileListView;

        public void SetTitle(string title) => _title.text = title;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked() => CloseRequest?.Invoke();
    }
}

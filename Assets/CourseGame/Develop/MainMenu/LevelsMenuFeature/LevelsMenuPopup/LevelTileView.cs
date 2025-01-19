using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CourseGame.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelTileView : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private Button _button;

        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _completeColor;
        [SerializeField] private Color _blockedColor;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetLevel(string level) => _levelNumberText.text = level;

        public void SetActive() => _background.color = _activeColor;

        public void SetComplete() => _background.color = _completeColor;

        public void SetBlock() => _background.color = _blockedColor;

        private void OnClick() => Clicked?.Invoke();
    }
}
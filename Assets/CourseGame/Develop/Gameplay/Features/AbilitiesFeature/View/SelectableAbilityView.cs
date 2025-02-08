using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View
{
    public class SelectableAbilityView : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Button _button;

        [SerializeField] private AbilityIcon _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private TMP_Text _textOnTablet;

        [SerializeField] private Image _selectImage;

        private Sequence _currentAnimation;
        private float _startYOffset = 100;

        public AbilityIcon Icon => _icon;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public void Subscribe()
        {
            _button.onClick.AddListener(OnClicked);
        }

        public void Unsubscribe()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public YieldInstruction Show()
        {
            _currentAnimation?.Kill();

            _currentAnimation = DOTween.Sequence();

            _currentAnimation
                .Append(_canvasGroup.DOFade(1, 0.4f))
                .Join(_canvasGroup.transform.DOLocalMoveY(0, 0.4f).From(_startYOffset))
                .SetUpdate(true);

            return _currentAnimation.WaitForCompletion();
        }

        public YieldInstruction Hide()
        {
            _currentAnimation?.Kill();

            _currentAnimation = DOTween.Sequence();

            _currentAnimation
                .Append(_canvasGroup.DOFade(0, 0.4f))
                .Join(_canvasGroup.transform.DOLocalMoveY(_startYOffset, 0.4f))
                .SetUpdate(true);

            return _currentAnimation.WaitForCompletion();
        }

        public void SetName(string name) => _name.text = name;

        public void SetDescription(string description) => _description.text = description;

        public void SetTabletText(string tabletText) => _textOnTablet.text = tabletText;

        public void Select() => _selectImage.gameObject.SetActive(true);

        public void Unselect() => _selectImage.gameObject.SetActive(false);

        private void OnClicked()
        {
            Clicked?.Invoke();
        }

        private void OnDestroy()
        {
            _currentAnimation?.Kill();
        }
    }
}

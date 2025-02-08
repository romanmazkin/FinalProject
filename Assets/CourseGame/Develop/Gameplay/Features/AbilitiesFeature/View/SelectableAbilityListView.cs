using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View
{
    public class SelectableAbilityListView : MonoBehaviour
    {
        public event Action SelectButtonClicked;

        [SerializeField] private Transform _parent;
        [SerializeField] private SelectableAbilityView _selectableAbilityViewPrefab;
        [SerializeField] private Button _selectButton;

        private List<SelectableAbilityView> _abilityViewElements = new();

        private Coroutine _animation;

        private void Awake()
        {
            _selectButton.gameObject.SetActive(false);
        }

        public void Subscribe()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        public void Unsubscribe()
        {
            _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        public void Select(SelectableAbilityView selectableAbilityView)
        {
            foreach (SelectableAbilityView view in _abilityViewElements)
                view.Unselect();

            selectableAbilityView.Select();
            _selectButton.gameObject.SetActive(true);
        }

        public SelectableAbilityView SpawnItem()
        {
            SelectableAbilityView selectableAbilityView = Instantiate(_selectableAbilityViewPrefab, _parent);
            _abilityViewElements.Add(selectableAbilityView);
            return selectableAbilityView;
        }

        public void Remove(SelectableAbilityView abilityView)
        {
            _abilityViewElements.Remove(abilityView);
            Destroy(abilityView.gameObject);
        }

        public YieldInstruction Show()
        {
            if(_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(ShowElements());
            return _animation;
        }

        private IEnumerator ShowElements()
        {
            YieldInstruction[] showAnimations = new YieldInstruction[_abilityViewElements.Count];

            for (int i = 0; i < _abilityViewElements.Count; i++)
            {
                showAnimations[i] = _abilityViewElements[i].Show();
                yield return new WaitForSecondsRealtime(0.25f);
            }

            foreach (YieldInstruction animation in showAnimations)
                yield return animation; 
        }

        public YieldInstruction Hide()
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _selectButton.gameObject.SetActive(false);
            _animation = StartCoroutine(HideElements());

            return _animation;
        }

        private IEnumerator HideElements()
        {
            YieldInstruction[] showAnimations = new YieldInstruction[_abilityViewElements.Count];

            for (int i = 0; i < _abilityViewElements.Count; i++)
            {
                showAnimations[i] = _abilityViewElements[i].Hide();
                yield return new WaitForSecondsRealtime(0.25f);
            }

            foreach (YieldInstruction animation in showAnimations)
                yield return animation;
        }

        private void OnSelectButtonClicked() => SelectButtonClicked?.Invoke();
    }
}

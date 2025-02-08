using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View
{
    public class AbilitySelectPopupView: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _selectAbilityText;

        [SerializeField] private SelectableAbilityListView _abilityListView;

        public SelectableAbilityListView AbilityListView => _abilityListView;

        public void SetTitle(string title) => _title.text = title;

        public void SetAdditionalText(string additionalText) => _selectAbilityText.text = additionalText;

        private Tween _animation;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public YieldInstruction Show()
        {
            _animation?.Kill();

            return StartCoroutine(ShowCoroutine());
        }

        public YieldInstruction Hide()
        {
            _animation?.Kill();

            return StartCoroutine(HideCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            _animation = _canvasGroup
                .DOFade(1, 0.8f)
                .From(0)
                .SetUpdate(true);

            yield return _animation.WaitForCompletion();
            yield return _abilityListView.Show();
        }

        private IEnumerator HideCoroutine()
        {
            yield return _abilityListView.Hide();

            _animation = _canvasGroup
                .DOFade(0, 0.8f)
                .From(1)
                .SetUpdate(true);

            yield return _animation.WaitForCompletion();
        }
    }
}

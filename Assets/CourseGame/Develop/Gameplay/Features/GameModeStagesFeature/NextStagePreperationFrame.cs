using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature
{
    public class NextStagePreperationFrame : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;

        public void Show(string text, Action callback)
        {
            _text.text = text;

            Sequence animation = DOTween.Sequence();

            animation
                .Append(_canvasGroup.DOFade(1, 1f).From(0))
                .Join(_canvasGroup.transform.DOLocalMove(_canvasGroup.transform.localPosition, 1f)
                    .From(_canvasGroup.transform.localPosition + Vector3.up * 100f))
                .Append(_canvasGroup.DOFade(0, 1f))
                .OnComplete(() =>
                {
                    callback?.Invoke();
                });
        }
    }
}

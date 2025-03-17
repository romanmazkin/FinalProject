using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    [RequireComponent(typeof(Animator))]
    public class AttackSpeedView : EntityView
    {
        private string AttackAnimationClipName = "Standing Draw Arrow";
        private string AttackAnimationMultiplierKey = "AttackAnimationMultiplier";

        [SerializeField] private Animator _animator;

        private ReactiveVariable<float> _attackInterval;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _attackInterval = entity.GetIntervalBetweenAttacks();

            _attackInterval.Changed += OnAttackIntervalChanged;
            UpdateAttackSpeedAnimation(_attackInterval.Value);
        }

        private void OnAttackIntervalChanged(float old, float newValue)
            => UpdateAttackSpeedAnimation(newValue);

        private void UpdateAttackSpeedAnimation(float attackInterval)
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

            float seconds = 0;

            foreach (AnimationClip clip in clips)
            {
                if (clip.name == AttackAnimationClipName)
                    seconds = clip.length;
            }

            _animator.SetFloat(AttackAnimationMultiplierKey, seconds / attackInterval + 0.1f);
        }

        private void OnDestroy()
        {
            _attackInterval.Changed -= OnAttackIntervalChanged;
        }
    }
}
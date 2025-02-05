using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    [RequireComponent(typeof(Animator))]

    public class DeadView : EntityView
    {
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;

        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _isDeathProcess;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _isDeathProcess = entity.GetIsDeathProcess();
            _isDead = entity.GetIsDead();

            _isDead.Changed += OnIsDeadChanged;
        }

        public void OnDeadAnimationEnded() => _isDeathProcess.Value = false;

        private void OnIsDeadChanged(bool arg1, bool isDead) => _animator.SetBool(IsDeadKey, isDead);

        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _isDead.Changed -= OnIsDeadChanged;
        }
    }
}


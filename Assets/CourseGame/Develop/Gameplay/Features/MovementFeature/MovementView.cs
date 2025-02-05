using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    [RequireComponent(typeof(Animator))]
    public class MovementView : EntityView
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsWalking");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isMoving;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _isMoving = entity.GetIsMoving();

            _isMoving.Changed += OnIsMovingChanged;
        }

        private void OnIsMovingChanged(bool arg1, bool isMoving) => _animator.SetBool(IsMovingKey, isMoving);

        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _isMoving.Changed -= OnIsMovingChanged;
        }
    }
}

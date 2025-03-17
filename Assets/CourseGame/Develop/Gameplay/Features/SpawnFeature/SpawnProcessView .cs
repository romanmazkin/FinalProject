using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.SpawnFeature
{
    [RequireComponent(typeof(Animator))]
    public class SpawnProcessView : EntityView
    {
        private readonly int SpawningProcessKey = Animator.StringToHash("IsSpawningProcess");

        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _spawnEffectPrefab;

        private ReactiveVariable<bool> _isSpawnProcess;
        private Transform _entityTransform;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _isSpawnProcess = entity.GetIsSpawningProcess();
            _entityTransform = entity.GetTransform();

            _isSpawnProcess.Changed += OnSpawnProcessChanged;
            UpdateSpawnProcessKey(_isSpawnProcess.Value);
        }

        private void OnSpawnProcessChanged(bool arg1, bool newValue) => UpdateSpawnProcessKey(newValue);

        private void UpdateSpawnProcessKey(bool value)
        {
            _animator.SetBool(SpawningProcessKey, value);

            if (value && _spawnEffectPrefab != null)
                Instantiate(_spawnEffectPrefab, _entityTransform.position, _spawnEffectPrefab.transform.rotation, null);
        }

        public void OnSpawnAnimationEnded() => _isSpawnProcess.Value = false;

        private void OnDestroy()
        {
            _isSpawnProcess.Changed -= OnSpawnProcessChanged;
        }
    }
}
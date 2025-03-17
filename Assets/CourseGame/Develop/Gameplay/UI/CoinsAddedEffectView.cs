using AssetKits.ParticleImage;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI
{
    public class CoinsAddedEffectView : MonoBehaviour
    {
        [SerializeField] private ParticleImage _effectPrefab;
        [SerializeField] private Transform _vfxLayer;
        [SerializeField] private Transform _target;

        public void SpawnEffect(Vector3 worldPosition)
        {
            Vector3 spawnEffectPosition = Camera.main.WorldToScreenPoint(worldPosition);

            ParticleImage particleImage = Instantiate(_effectPrefab, spawnEffectPosition, Quaternion.identity, _vfxLayer);
            particleImage.attractorTarget = _target;
        }
    }
}

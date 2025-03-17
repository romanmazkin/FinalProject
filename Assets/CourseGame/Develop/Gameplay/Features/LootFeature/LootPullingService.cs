using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class LootPullingService : IDisposable
    {
        private ReactiveEvent _allCollected = new();

        private List<Entity> _loot = new();

        private EntitiesBuffer _entitiesBuffer;

        private bool _activate;

        public LootPullingService(EntitiesBuffer entitiesBuffer)
        {
            _entitiesBuffer = entitiesBuffer;

            _entitiesBuffer.Added += OnEntitiesAdded;
            _entitiesBuffer.Removed += OnEntitiesRemoved;
        }

        public void PullTo(Entity entity)
        {
            if (_activate)
                throw new InvalidOperationException();

            _activate = true;

            if (_loot.Count == 0)
            {
                _allCollected?.Invoke();
                return;
            }

            foreach (Entity loot in _loot)
            {
                loot.GetTarget().Value = entity;
                loot.GetIsPullingProcess().Value = true;
            }
        }

        public void Reset() => _activate = false;

        private void OnEntitiesRemoved(Entity entity)
        {
            _loot.Remove(entity);

            if(_loot.Count == 0)
            {
                _allCollected?.Invoke();
            }
        }

        private void OnEntitiesAdded(Entity entity)
        {
            if(entity.TryGetIsPullable(out var isPullable) && isPullable.Value)
            {
                _loot.Add(entity);

                Transform lootTransform = entity.GetTransform();

                Vector2 randomOffset = UnityEngine.Random.insideUnitCircle;
                Vector3 offset = new Vector3(randomOffset.x, 0, randomOffset.y);
                Vector3 endJumpPosition = lootTransform.position + offset;

                lootTransform
                    .DOJump(endJumpPosition, 2, 1, 0.7f)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() => entity.GetIsSpawningProcess().Value = false)
                    .Play();
            }
        }

        public IReadOnlyEvent AllCollected => _allCollected;

        public bool Activated => _activate;

        public void Dispose()
        {
            _entitiesBuffer.Added -= OnEntitiesAdded;
            _entitiesBuffer.Removed -= OnEntitiesRemoved;
        }
    }
}
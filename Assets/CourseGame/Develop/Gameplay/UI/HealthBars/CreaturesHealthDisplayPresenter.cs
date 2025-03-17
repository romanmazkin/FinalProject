using Assets.CourseGame.Develop.CommonUI;
using Assets.CourseGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI.HealthBars
{
    public class CreaturesHealthDisplayPresenter : IDisposable, IInitializeable
    {
        private EntitiesBuffer _entitesBuffer;
        private CreaturesHealthDisplay _healthDisplay;

        private HealthBarFactory _healthBarFactory;

        private Dictionary<EntityToRemoveReason, EntityHealthPresenter> _presenters = new();

        public CreaturesHealthDisplayPresenter(
            EntitiesBuffer creaturesBuffer,
            CreaturesHealthDisplay healthDisplay,
            HealthBarFactory healthBarFactory)
        {
            _entitesBuffer = creaturesBuffer;
            _healthDisplay = healthDisplay;
            _healthBarFactory = healthBarFactory;
        }

        public void Initialize()
        {
            Enable();
        }

        public void Dispose()
        {
            Disable();
        }

        public void Enable()
        {
            _entitesBuffer.Added += OnCreatureAdded;

            foreach (Entity entity in _entitesBuffer.Elements)
                OnCreatureAdded(entity);
        }

        public void Disable()
        {
            _entitesBuffer.Added -= OnCreatureAdded;

            foreach (var presenter in _presenters)
                RemoveBar(presenter.Key, presenter.Value);

            _presenters.Clear();

            UnityEngine.Object.Destroy(_healthDisplay.gameObject);
        }

        public void LateUpdate()
        {
            foreach (var presenter in _presenters)
                _healthDisplay.UpdatePositionFor(presenter.Value.Bar, presenter.Key.Entity.GetHealthBarPoint().position);
        }

        private void OnCreatureAdded(Entity entity)
        {
            if (entity.TryGetHealthBarPoint(out Transform healthBarPoint))
            {
                ReactiveVariable<float> health = entity.GetHealth();
                ReactiveVariable<float> maxHealth = entity.GetMaxHealth();
                ReactiveVariable<int> team = entity.GetTeam();

                BarWithText healthBar = null;

                if (entity.TryGetIsMainHero(out var isMainHero) && isMainHero.Value)
                    healthBar = _healthBarFactory.CreateHeroHealthBar();
                else
                    healthBar = _healthBarFactory.CreateSimpleHealthBar();

                _healthDisplay.Attach(healthBar);

                EntityHealthPresenter entityHealthPresenter = new EntityHealthPresenter(health, maxHealth, team, healthBar);
                entityHealthPresenter.Enable();

                EntityToRemoveReason entityToRemoveReason = new EntityToRemoveReason(entity);
                entityToRemoveReason.OnRemoveReasonComplete += OnRemoveReasonComplete;

                _presenters.Add(entityToRemoveReason, entityHealthPresenter);
            }
        }

        private void OnRemoveReasonComplete(EntityToRemoveReason removeReason)
        {
            if (_presenters.TryGetValue(removeReason, out var presenter))
                RemoveBar(removeReason, presenter);
        }

        private void RemoveBar(EntityToRemoveReason removeReason, EntityHealthPresenter presenter)
        {
            presenter.Disable();
            removeReason.OnRemoveReasonComplete -= OnRemoveReasonComplete;
            removeReason.Dispose();
            _healthDisplay.UnAttached(presenter.Bar);

            if ((presenter.Bar as UnityEngine.Object) != null)
                UnityEngine.Object.Destroy(presenter.Bar.gameObject);

            _presenters.Remove(removeReason);
        }

        private class EntityToRemoveReason : IDisposable
        {
            public event Action<EntityToRemoveReason> OnRemoveReasonComplete;

            public EntityToRemoveReason(Entity entity)
            {
                Entity = entity;
                Entity.GetIsDead().Changed += OnEntityDied;
                Entity.Disposed += OnEntityDisposed;
            }

            private void OnEntityDisposed(Entity entity)
            {
                Dispose();
                OnRemoveReasonComplete?.Invoke(this);
            }

            public Entity Entity { get; private set; }

            private void OnEntityDied(bool arg1, bool isDead)
            {
                if (isDead)
                {
                    Dispose();
                    OnRemoveReasonComplete?.Invoke(this);
                }
            }

            public void Dispose()
            {
                Entity.Disposed -= OnEntityDisposed;
                Entity.GetIsDead().Changed -= OnEntityDied;
            }
        }
    }
}
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class NextStagePreparationState : State, IUpdatableState
    {
        private EntityFactory _entityFactory;
        private Entity _nextStageTrigger;

        private IDisposable _nextStageTriggerDisposable;

        public NextStagePreparationState(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public ReactiveEvent OnNextStageTriggerComplete { get; } = new();

        public override void Enter()
        {
            base.Enter();

            _nextStageTrigger = _entityFactory.CreateNextGameplayStageTrigger(Vector3.zero + Vector3.forward * 4);
            _nextStageTriggerDisposable = _nextStageTrigger.GetSelfTriggerReciever().Enter.Subscribe(OnNextStageTriggerEntered);
        }

        private void OnNextStageTriggerEntered(Collider collider)
        {
            if (collider.TryGetComponent(out Entity entity) && entity.TryGetIsMainHero(out var isMainHero) && isMainHero.Value)
                OnNextStageTriggerComplete.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            _nextStageTriggerDisposable.Dispose();
            UnityEngine.Object.Destroy(_nextStageTrigger.gameObject);
        }

        public void Update(float deltaTime)
        {
        }
    }
}

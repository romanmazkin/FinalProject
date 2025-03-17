using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class NextStagePreperationState : State, IUpdatableState
    {
        private EntityFactory _entityFactory;
        private Entity _nextStageTrigger;

        private StageProviderService _stageProviderService;
        private NextStagePreperationFrameFactory _nextStagePreperationFrameFactory;

        private IDisposable _nextStageTriggerDisposable;


        public NextStagePreperationState(
            EntityFactory entityFactory,
            StageProviderService stageProviderService,
            NextStagePreperationFrameFactory nextStagePreperationFrameFactory)
        {
            _entityFactory = entityFactory;
            _stageProviderService = stageProviderService;
            _nextStagePreperationFrameFactory = nextStagePreperationFrameFactory;
        }

        public ReactiveEvent OnNextStageTriggerComplete { get; } = new();
        public override void Enter()
        {
            base.Enter();

            NextStagePreperationFrame nextStageFrame = _nextStagePreperationFrameFactory.CreateFrame();

            if (_stageProviderService.HasNextStage() || _stageProviderService.StageResult == StageResult.Uncompleted)
                nextStageFrame.Show("Go to next stage!", () => Object.Destroy(nextStageFrame.gameObject));
            else
                nextStageFrame.Show("Go to next level!", () => Object.Destroy(nextStageFrame.gameObject));

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

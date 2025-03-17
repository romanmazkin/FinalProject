using Assets.CourseGame.Develop.Gameplay.Features.LootFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class CollectLootState : State, IUpdatableState
    {
        private ReactiveEvent _lootCollected = new();

        private LootPullingService _lootPullingService;
        private MainHeroHolderService _mainHeroHolderService;

        private IDisposable _pullingDisposable;

        public CollectLootState(
            LootPullingService lootPullingService,
            MainHeroHolderService mainHeroHolderService)
        {
            _lootPullingService = lootPullingService;
            _mainHeroHolderService = mainHeroHolderService;
        }

        public IReadOnlyEvent LootCollected => _lootCollected;

        public override void Enter()
        {
            base.Enter();

            _pullingDisposable = _lootPullingService.AllCollected.Subscribe(OnLootCollected);
            _lootPullingService.PullTo(_mainHeroHolderService.MainHero);
        }

        public override void Exit()
        {
            base.Exit();

            _pullingDisposable?.Dispose();
            _lootPullingService.Reset();
        }

        private void OnLootCollected()
        {
            _lootCollected?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
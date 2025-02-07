using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.States;
using Assets.CourseGame.Develop.Utils.Conditions;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroFinishConditionCreator : IDisposable
    {
        private MainHeroHolderService _heroHolderService;
        private GameplayFinishConditionService _gameplayFinishConditionService;

        private IDisposable _heroRegistredDisposable;
        private IDisposable _heroUnregistredDisposable;

        private ICondition _defeatCondition;

        public MainHeroFinishConditionCreator(
            MainHeroHolderService heroHolderService,
            GameplayFinishConditionService gameplayFinishConditionService)
        {
            _heroHolderService = heroHolderService;
            _gameplayFinishConditionService = gameplayFinishConditionService;

            _heroRegistredDisposable = _heroHolderService.HeroRegistred.Subscribe(OnHeroRegistred);
            _heroUnregistredDisposable = _heroHolderService.HeroUnregistred.Subscribe(OnHeroUnregistred);
        }

        private void OnHeroRegistred(Entity hero)
        {
            _defeatCondition = new FuncCondition(() => hero.GetIsDead().Value);
            _gameplayFinishConditionService.DefeatCondition.Add(_defeatCondition);
        }

        private void OnHeroUnregistred(Entity hero)
        {
            _gameplayFinishConditionService.DefeatCondition.Remove(_defeatCondition);
            _defeatCondition = null;
        }

        public void Dispose()
        {
            _heroRegistredDisposable.Dispose();
            _heroUnregistredDisposable.Dispose();
        }
    }
}
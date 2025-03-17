using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature;
using Assets.CourseGame.Develop.Gameplay.UI;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroExperienceBarCreatorService : IDisposable
    {
        private MainHeroHolderService _mainHeroHolderService;
        private GameplayUIFactory _gameplayUIFactory;
        private ExperiencePresenter _entityExperiencePresenter;

        private IDisposable _heroRegistredDisposable;
        private IDisposable _heroUnregistredDisposable;

        public MainHeroExperienceBarCreatorService(
            MainHeroHolderService mainHeroHolderService,
            GameplayUIFactory gameplayUIFactory)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _heroRegistredDisposable = _mainHeroHolderService.HeroRegistred.Subscribe(OnMainHeroRegister);
            _heroUnregistredDisposable = _mainHeroHolderService.HeroUnregistred.Subscribe(OnMainHeroUnregister);
            _gameplayUIFactory = gameplayUIFactory;
        }

        private void OnMainHeroUnregister(Entity hero)
        {
            _entityExperiencePresenter.Disable();
            _entityExperiencePresenter = null;
        }

        private void OnMainHeroRegister(Entity hero)
        {
            _entityExperiencePresenter = _gameplayUIFactory.CreateMainHeroExperiencePresenter(hero);
            _entityExperiencePresenter.Enable();
        }

        public void Dispose()
        {
            _heroRegistredDisposable.Dispose();
            _heroUnregistredDisposable.Dispose();
        }
    }
}
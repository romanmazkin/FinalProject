using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonUI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI
{
    public class GameplayUIFactory
    {
        private ResourcesAssetLoader _resourcesAssetLoader;
        private GameplayUIRoot _gameplayUIRoot;
        private ConfigsProviderService _configsProvider;
        private DIContainer _container;

        public GameplayUIFactory(DIContainer container)
        {
            _resourcesAssetLoader = container.Resolve<ResourcesAssetLoader>();
            _gameplayUIRoot = container.Resolve<GameplayUIRoot>();
            _configsProvider = container.Resolve<ConfigsProviderService>();
            _container = container;
        }

        public ExperiencePresenter CreateMainHeroExperiencePresenter(Entity entity)
        {
            BarWithText expBarPrefab = _resourcesAssetLoader.LoadResource<BarWithText>("Gameplay/UI/ExpBar");
            BarWithText expBar = Object.Instantiate(expBarPrefab, _gameplayUIRoot.HUDLayer);
            return new ExperiencePresenter(entity, expBar, _configsProvider.ExperienceForUpgradeLevelConfig);
        }

        public StagePresenter CreateStagePresenter()
        {
            IconWithText iconWithTextPrefab = _resourcesAssetLoader.LoadResource<IconWithText>("Gameplay/UI/StageView");
            IconWithText iconWithText = Object.Instantiate(iconWithTextPrefab, _gameplayUIRoot.HUDLayer);
            return new StagePresenter(iconWithText, _container.Resolve<StageProviderService>());
        }
    }
}

using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonUI;

namespace Assets.CourseGame.Develop.Gameplay.UI.HealthBars
{
    public class HealthBarFactory
    {
        private const string SimpleHealthBarPath = "Gameplay/UI/HealthBars/SimpleHealthBar";
        private const string HeroHealthBarPath = "Gameplay/UI/HealthBars/HeroHealthBar";
        private const string HealthDisplayPath = "Gameplay/UI/HealthBars/HealthBarsDisplay";
        
        private readonly DIContainer _container;
        private ResourcesAssetLoader _resourcesAssetLoader;

        public HealthBarFactory(DIContainer container)
        {
            _container = container;
            _resourcesAssetLoader = _container.Resolve<ResourcesAssetLoader>();
        }

        public BarWithText CreateSimpleHealthBar()
        {
            BarWithText prefab = _resourcesAssetLoader.LoadResource<BarWithText>(SimpleHealthBarPath);

            return UnityEngine.Object.Instantiate(prefab);
        }

        public BarWithText CreateHeroHealthBar()
        {
            BarWithText prefab = _resourcesAssetLoader.LoadResource<BarWithText>(HeroHealthBarPath);

            return UnityEngine.Object.Instantiate(prefab);
        }

        public CreaturesHealthDisplay CreaturesHealthDisplay()
        {
            CreaturesHealthDisplay creaturesHealthDisplayPrefab = _resourcesAssetLoader.LoadResource<CreaturesHealthDisplay>(HealthDisplayPath);

            return UnityEngine.Object.Instantiate(creaturesHealthDisplayPrefab, _container.Resolve<GameplayUIRoot>().HUDLayer);
        }
    }
}
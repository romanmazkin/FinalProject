using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.Gameplay.UI;

namespace Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature
{
    public class NextStagePreperationFrameFactory
    {
        private GameplayUIRoot _gameplayUIRoot;
        private ResourcesAssetLoader _resourcesAssetLoader;

        public NextStagePreperationFrameFactory(DIContainer container)
        {
            _gameplayUIRoot = container.Resolve<GameplayUIRoot>();
            _resourcesAssetLoader = container.Resolve<ResourcesAssetLoader>();
        }

        public NextStagePreperationFrame CreateFrame()
        {
            NextStagePreperationFrame nextStagePreperationFramePrefab = _resourcesAssetLoader
                .LoadResource<NextStagePreperationFrame>("Gameplay/UI/SimpleWaveFrame");
            return UnityEngine.Object.Instantiate(nextStagePreperationFramePrefab, _gameplayUIRoot.PopupsLayer);
        }
    }
}

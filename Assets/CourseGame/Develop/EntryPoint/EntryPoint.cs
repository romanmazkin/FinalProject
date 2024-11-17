using System;
using UnityEngine;
using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using System.ComponentModel;
using Assets.CourseGame.Develop.CommonServices.LoadingScreen;

namespace Assets.CourseGame.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();

            // registration services for all project
            // global context analog
            // parent container creation

            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoruotinePerrformer(projectContainer);
            RegisterSceneLoader(projectContainer);

            // all registrations done

            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }

        private void RegisterResourcesAssetLoader(DIContainer projectContainer)
        {
            projectContainer.RegisterAsSingle(c => new ResourcesAssetLoader());
        }

        private void RegisterCoruotinePerrformer(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                CoroutinePerformer coroutinePerformerPrefab = resourcesAssetLoader.
                LoadResource<CoroutinePerformer>(InfrastructureAssetPaths.CoroutinePerformerPath);

                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private void RegisterSceneLoader(DIContainer container)
        {
            container.RegisterAsSingle<ILoadingCurrtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                StandartLoadingCurtain standartLoadingCurtainPrefab = resourcesAssetLoader.
                LoadResource<StandartLoadingCurtain>(InfrastructureAssetPaths.StandartLoadingCurtainPath);

                return Instantiate(standartLoadingCurtainPrefab);
            });
        }
    }
}

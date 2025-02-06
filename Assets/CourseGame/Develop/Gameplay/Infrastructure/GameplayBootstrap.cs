using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using System.Collections;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"asdfklhsdf {gameplayInputArgs.LevelNumber}");
            Debug.Log("sdfsdfsdf");
            Debug.Log("sdfsdfsdf");

            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
            var gameMode = _container.Resolve<GameModesFactory>().CreateWaveGameMode();
            gameMode.Start(configsProviderService.LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber).WaveConfigs[0]);

            yield return new WaitForSeconds(1f);
        }

        private void ProcessRegistrations()
        {
            _container.RegisterAsSingle<IInputService>(c => new DesktopInput());

            _container.RegisterAsSingle(c => new EntitiesBuffer());

            _container.RegisterAsSingle(c => new EntityFactory(c));
            _container.RegisterAsSingle(c => new AIFactory(c));
            _container.RegisterAsSingle(c => new EnemyFactory(c));
            _container.RegisterAsSingle(c => new MainHeroFactory(c));
            _container.RegisterAsSingle(c => new GameModesFactory(c));

            _container.Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
            }
        }
    }
}

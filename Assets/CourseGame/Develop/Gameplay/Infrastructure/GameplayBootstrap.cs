using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using System.Collections;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        [SerializeField] private GameplayTest _gameplayTest;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"asdfklhsdf {gameplayInputArgs.LevelNumber}");
            Debug.Log("sdfsdfsdf");
            Debug.Log("sdfsdfsdf");

            _gameplayTest.StartProcess(_container);

            yield return new WaitForSeconds(1f);
        }

        private void ProcessRegistrations()
        {
            _container.RegisterAsSingle(c => new EntityFactory(c));
            _container.RegisterAsSingle(c => new AIFactory(c));

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

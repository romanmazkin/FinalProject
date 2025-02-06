using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        public void StartProcess(DIContainer container)
        {
            _container = container;

            _container.Resolve<MainHeroFactory>().Create(Vector3.zero);
            _container.Resolve<EnemyFactory>().CreateGhost(Vector3.zero + Vector3.forward * 4);
        }
    }
}
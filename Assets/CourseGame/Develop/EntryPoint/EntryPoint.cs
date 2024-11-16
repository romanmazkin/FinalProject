using System;
using UnityEngine;
using Assets.CourceGame.Develop.DI;

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

            // laaunch gameBootstrap from coroutine method Run
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }
    }
}

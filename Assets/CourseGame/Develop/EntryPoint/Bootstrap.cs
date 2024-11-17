using System;
using System.Collections;
using UnityEngine;
using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.LoadingScreen;

namespace Assets.CourseGame.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            ILoadingCurrtain loadingCurrtain = container.Resolve<ILoadingCurrtain>();

            loadingCurrtain.Show();

            Debug.Log("Run services initialization");

            //initialize all services

            yield return new WaitForSeconds(1.5f);

            //disable launch display

            Debug.Log("Services initialization is done");

            loadingCurrtain.Hide();

            // switch to next scene with scene cwitcher
        }
    }
}

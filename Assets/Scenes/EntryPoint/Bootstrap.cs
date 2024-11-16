using System;
using System.Collections;
using UnityEngine;
using Assets.CourceGame.Develop.DI;

namespace Assets.CourseGame.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            //enable launch display after all registrations

            //initialize all services

            yield return new WaitForSeconds(1.5f);

            //disable launch display

            // switch to next scene with scene cwitcher
        }
    }
}

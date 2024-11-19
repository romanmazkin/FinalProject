using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using System.Collections;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        yield return new WaitForSeconds(1f);
    }

    private void ProcessRegistrations()
    {
        //registrations foe this scene
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
        }
    }
}

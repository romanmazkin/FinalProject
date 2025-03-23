using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private SceneSwitcher _sceneSwitcher;

        public DefeatState(
            SceneSwitcher sceneSwitcher,
            IPauseService pauseService, 
            IInputService inputService) : base(pauseService, inputService)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Lose");
        }

        public void Update(float deltaTime)
        {
                _sceneSwitcher.ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
        }
    }
}

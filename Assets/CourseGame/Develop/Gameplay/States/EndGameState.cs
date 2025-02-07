using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;
using Assets.CourseGame.Develop.Utils.StateMachineBase;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public abstract class EndGameState : State
    {
        private IPauseService _pauseService;
        private IInputService _inputService;

        public EndGameState(IPauseService pauseService, IInputService inputService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();

            _pauseService.Pause();
            _inputService.IsEnabled = false;
        }

        public override void Exit()
        {
            base.Exit();

            _pauseService.Unpause();
            _inputService.IsEnabled = true;
        }
    }
}

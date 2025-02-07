using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.CommonServices.DataManagement.DataProviders;
using Assets.CourseGame.Develop.CommonServices.LevelsManagement;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class GameplayStateFactory
    {
        private DIContainer _container;

        public GameplayStateFactory(DIContainer container)
        {
            _container = container;
        }

        public InitMainCharacterState CreateMainCharacterState()
        {
            return new InitMainCharacterState(_container.Resolve<MainHeroFactory>(), _container.Resolve<ConfigsProviderService>());
        }

        public NextStagePreparationState CreateNextStagePreparationState()
        {
            return new NextStagePreparationState(_container.Resolve<EntityFactory>());
        }

        public StageProcessState CreateStageProcessState(GameplayInputArgs gameplayInputArgs)
        {
            return new StageProcessState(
                _container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplayInputArgs.LevelNumber),
                _container.Resolve<GameModesFactory>(),
                _container.Resolve<StageProviderService>());
        }

        public WinState CreateWinState(GameplayInputArgs gameplayInputArgs)
        {
            return new WinState(
                _container.Resolve<CompletedLevelsService>(),
                _container.Resolve<PlayerDataProvider>(),
                gameplayInputArgs,
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<IPauseService>(),
                _container.Resolve<IInputService>());
        }

        public DefeatState CreateDefeatState()
        {
            return new DefeatState(
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<IPauseService>(),
                _container.Resolve<IInputService>());
        }
    }
}

using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.Features.EnemiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature.WaveGameModeFeature;

namespace Assets.CourseGame.Develop.Gameplay.Features.GameModeStagesFeature
{
    public class GameModesFactory
    {
        private DIContainer _container;

        public GameModesFactory(DIContainer container)
        {
            _container = container;
        }

        public WaveGameMode CreateWaveGameMode()
        {
            return new WaveGameMode(_container.Resolve<EnemyFactory>());
        }
    }
}

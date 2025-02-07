using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;

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
    }
}

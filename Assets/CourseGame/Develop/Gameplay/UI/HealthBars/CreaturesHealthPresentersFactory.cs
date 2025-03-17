using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.Entities;

namespace Assets.CourseGame.Develop.Gameplay.UI.HealthBars
{
    public class CreaturesHealthPresentersFactory
    {
        private DIContainer _container;

        public CreaturesHealthPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CreaturesHealthDisplayPresenter CreateHealthDisplayPresenter()
        {
            CreaturesHealthDisplay creaturesHealthDisplay = _container.Resolve<HealthBarFactory>().CreaturesHealthDisplay();
            return new CreaturesHealthDisplayPresenter(
                _container.Resolve<EntitiesBuffer>(),
                creaturesHealthDisplay,
                _container.Resolve<HealthBarFactory>());
        }
    }
}

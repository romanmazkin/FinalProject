using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;

namespace Assets.CourseGame.Develop.CommonServices.Timer
{
    public class TimerServiceFactory
    {
        private DIContainer _container;

        public TimerServiceFactory(DIContainer container)
        {
            _container = container;
        }

        public TimerService Create(float cooldown)
            => new TimerService(cooldown, _container.Resolve<ICoroutinePerformer>());
    }
}

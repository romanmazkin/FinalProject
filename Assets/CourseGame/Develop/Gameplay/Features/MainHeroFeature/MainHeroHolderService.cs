using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature
{
    public class MainHeroHolderService : IDisposable
    {
        private ReactiveEvent<Entity> _heroRegistred = new();
        private ReactiveEvent<Entity> _heroUnregistred = new();

        private Entity _mainHero;

        public IReadOnlyEvent<Entity> HeroRegistred => _heroRegistred;
        public IReadOnlyEvent<Entity> HeroUnregistred => _heroUnregistred;
        public Entity MainHero => _mainHero;

        public void Dispose()
        {
            _mainHero = null;
        }

        public void Register(Entity mainHero)
        {
            if(_mainHero != null)
                throw new ArgumentException(nameof(mainHero));

            _mainHero = mainHero;
            _heroRegistred?.Invoke(mainHero);
        }

        public void Unregister(Entity mainHero)
        {
            if(_mainHero == null)
                throw new InvalidOperationException();

            _heroUnregistred?.Invoke(mainHero);
            _mainHero = null;
        }
    }
}

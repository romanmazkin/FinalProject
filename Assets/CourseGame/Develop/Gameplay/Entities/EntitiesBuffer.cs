using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public class EntitiesBuffer : ObservableList<Entity>, IDisposable
    {
        public override void Add(Entity entity)
        {
            base.Add(entity);

            entity.Disposed += OnElementDisposed;
        }

        public void Dispose()
        {
            foreach (var entity in Elements)
                entity.Disposed -= OnElementDisposed;

            Clear();
        }

        private void OnElementDisposed(Entity entity)
        {
            entity.Disposed -= OnElementDisposed;
            Remove(entity);
        }
    }
}

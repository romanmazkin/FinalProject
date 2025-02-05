using UnityEngine;
namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public abstract class EntityView : MonoBehaviour
    {
        public void SubscribeTo(Entity entity)
        {
            entity.Initialized += OnEntityInitialized;
            entity.Disposed += OnEntityDisposed;
        }

        protected virtual void OnEntityDisposed(Entity entity)
        {
            entity.Disposed -= OnEntityDisposed;
            entity.Initialized -= OnEntityInitialized;
        }

        protected abstract void OnEntityInitialized(Entity entity);
    }
}
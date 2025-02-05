using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class EntityViewRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private GameObject _rootView;

        public override void Register(Entity entity)
        {
            foreach (EntityView entityView in _rootView.GetComponentsInChildren<EntityView>())
                entityView.SubscribeTo(entity);
        }
    }
}
using UnityEngine;
namespace Assets.CourseGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class RigidbodyEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Rigidbody _rigidbody;
        public override void Register(Entity entity)
        {
            entity.AddRigidbody(_rigidbody);
        }
    }
}
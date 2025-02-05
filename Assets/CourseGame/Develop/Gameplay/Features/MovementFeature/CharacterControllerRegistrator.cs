using Assets.CourseGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.MovementFeature
{
    public class CharacterControllerRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CharacterController _characterController;

        public override void Register(Entity entity)
        {
            entity.AddValue(EntityValues.CharacterController, _characterController);
        }
    }
}

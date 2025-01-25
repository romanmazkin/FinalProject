using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _ghost;

        public void StartProcerss(DIContainer container)
        {
            _container = container;

            _ghost = _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero);

            _ghost.TryGetValue(EntityValues.MoveSpeed, out ReactiveVariable<float> moveSpeed);
            Debug.Log($"Speed is {moveSpeed.Value}");
        }

        private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if(_ghost != null)
            {
                _ghost.TryGetValue(EntityValues.MoveDirection, out ReactiveVariable<Vector3> moveDirection);
                _ghost.TryGetValue(EntityValues.RotationDirection, out ReactiveVariable<Vector3> rotationDirection);

                moveDirection.Value = input;
                rotationDirection.Value = input;
            }
        }
    }
}

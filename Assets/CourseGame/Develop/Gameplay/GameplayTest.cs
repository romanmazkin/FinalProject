using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.Gameplay.AI;
using Assets.CourseGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _ghost;

        private bool _isPlayerInput = true;

        public void StartProcess(DIContainer container)
        {
            _container = container;

            _ghost = _container.Resolve<EntityFactory>().CreateMainHero(Vector3.zero);
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.forward * 4);

            Debug.Log($"Скорость созданного призрака: {_ghost.GetMoveSpeed().Value}");
        }

        private void Update()
        {
            if (_isPlayerInput)
            {
                ProcessPlayerInput();
                if (Input.GetKeyDown(KeyCode.G))
                {
                    _isPlayerInput = false;

                    _ghost.GetMoveDirection().Value = Vector3.zero;
                    _ghost.GetRotationDirection().Value = Vector3.zero;

                    AIStateMachine ghostBehaviour = _container.Resolve<AIFactory>().CreateGhostBehaviour(_ghost);

                    _ghost.AddBehaviour(new StateMachineBrainBehaviour(ghostBehaviour));
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    _isPlayerInput = true;
                    _ghost.TryRemoveBehaviour<StateMachineBrainBehaviour>();
                }
            }
        }

        private void ProcessPlayerInput()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (_ghost != null)
            {
                _ghost.GetMoveDirection().Value = input;
                _ghost.GetRotationDirection().Value = input;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _ghost.GetAttackTrigger().Invoke();
                }
            }
        }
    }
}
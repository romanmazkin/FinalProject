using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.CourseGame.Develop.Gameplay.AI.States
{
    public class RandomDirectionGenerateState : State, IUpdatableState
    {
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveVariable<Vector3> _rotationDirection;

        private float _time = 0;

        private float _cooldownBetweenDirectionGeneration;

        public RandomDirectionGenerateState(
            ReactiveVariable<Vector3> movementDirection,
            ReactiveVariable<Vector3> rotationDirection,
            float cooldownBetweenDirectionGeneration)
        {
            _movementDirection = movementDirection;
            _rotationDirection = rotationDirection;
            _cooldownBetweenDirectionGeneration = cooldownBetweenDirectionGeneration;
        }

        public override void Enter()
        {
            base.Enter();

            _movementDirection.Value = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            _rotationDirection.Value = _movementDirection.Value;
            _time = 0;
        }

        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time > _cooldownBetweenDirectionGeneration)
            {
                GenerateNewDirection();
                _time = 0;
            }
        }

        private void GenerateNewDirection()
        {
            _movementDirection.Value = Quaternion.Euler(0, Random.Range(-30, 30), 0) * (-_movementDirection.Value.normalized);
            _rotationDirection.Value = _movementDirection.Value;
        }
    }
}
using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;
using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.AI.States
{
    public class RotateToTargetState : State, IUpdatableState
    {
        private ReactiveVariable<Vector3> _rotationDirection;
        private ITargetSelector _targetSelector;
        private Transform _source;
        private List<Entity> _detectedTargets;

        public RotateToTargetState(
            ReactiveVariable<Vector3> rotationDirection, 
            ITargetSelector targetSelector, 
            Transform source, 
            List<Entity> detectedTargets)
        {
            _rotationDirection = rotationDirection;
            _targetSelector = targetSelector;
            _source = source;
            _detectedTargets = detectedTargets;
        }

        public void Update(float deltaTime)
        {
            if(_targetSelector.TrySelectTarget(_detectedTargets, out Entity findedTarget))
                if(findedTarget.TryGetTransform(out Transform targetTransform))
                    _rotationDirection.Value = targetTransform.position - _source.position;
        }
    }
}

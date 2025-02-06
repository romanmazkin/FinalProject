using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.Timer;
using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.AI.States;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.InputFeature;
using Assets.CourseGame.Develop.Utils.Conditions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.AI
{
    public class AIFactory
    {
        private DIContainer _container;
        private TimerServiceFactory _timerServiceFactory;

        public AIFactory(DIContainer container)
        {
            _container = container;
            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
        }

        public AIStateMachine CreateMainHeroBehaviour(Entity entity, ITargetSelector targetSelector)
        {
            AIStateMachine automaticAttackState = AutoRotateAttackStateMachineCreate(entity, targetSelector);

            var moveDirection = entity.GetMoveDirection();
            var rotationDirection = entity.GetRotationDirection();
            IInputService inputService = _container.Resolve<IInputService>();

            PlayerDirectionGenerateState playerDirectionGenerateState = new PlayerDirectionGenerateState(inputService, moveDirection, rotationDirection);

            ICompositeCondition fromNotCombatStateToAttackStateCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => targetSelector.TrySelectTarget(entity.GetDetectedEntitiesBuffer(), out Entity findedEntity)))
                .Add(new FuncCondition(() => inputService.Direction == Vector3.zero));

            ICompositeCondition fromAttackStateToNotCombatStateCondition = new CompositeCondition(LogicOperations.OrOperation)
                .Add(new FuncCondition(() => targetSelector.TrySelectTarget(entity.GetDetectedEntitiesBuffer(), out Entity findedEntity) == false))
                .Add(new FuncCondition(() => inputService.Direction != Vector3.zero));

            AIStateMachine rootStateMachine = new AIStateMachine();

            rootStateMachine.AddState(playerDirectionGenerateState);
            rootStateMachine.AddState(automaticAttackState);

            rootStateMachine.AddTransition(playerDirectionGenerateState, automaticAttackState, fromNotCombatStateToAttackStateCondition);
            rootStateMachine.AddTransition(automaticAttackState, playerDirectionGenerateState, fromAttackStateToNotCombatStateCondition);

            return rootStateMachine;
        }

        public AIStateMachine CreateGhostBehaviour(Entity entity)
            => CreateRandomMovementStateMachine(entity);

        private AIStateMachine AutoRotateAttackStateMachineCreate(Entity entity, ITargetSelector targetSelector)
        {
            var rotationDirection = entity.GetRotationDirection();
            var transform = entity.GetTransform();

            RotateToTargetState rotateToTargetState = new RotateToTargetState(
                rotationDirection,
                targetSelector,
                transform,
                entity.GetDetectedEntitiesBuffer());

            var attackTrigger = entity.GetAttackTrigger();

            AttackTriggerState attackTriggerState = new AttackTriggerState(attackTrigger);

            var canAttack = entity.GetAttackCondition();

            ICompositeCondition fromRotateToAttackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(canAttack)
                .Add(new FuncCondition(() =>
                {
                    if (targetSelector.TrySelectTarget(entity.GetDetectedEntitiesBuffer(), out Entity findedTarget))
                        return Quaternion.Angle(transform.rotation, Quaternion.LookRotation(findedTarget.GetTransform().position - transform.position)) < 1f;

                    return false;
                }));

            var attackProcess = entity.GetIsAttackProcess();

            ICompositeCondition fromAttackToRotateStateCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => attackProcess.Value == false));

            AIStateMachine automaticAttackState = new AIStateMachine();

            automaticAttackState.AddState(rotateToTargetState);
            automaticAttackState.AddState(attackTriggerState);

            automaticAttackState.AddTransition(rotateToTargetState, attackTriggerState, fromRotateToAttackCondition);
            automaticAttackState.AddTransition(attackTriggerState, rotateToTargetState, fromAttackToRotateStateCondition);

            return automaticAttackState;
        }

        private AIStateMachine CreateRandomMovementStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            var moveDirection = entity.GetMoveDirection();
            var rotationDirection = entity.GetRotationDirection();

            RandomDirectionGenerateState randomDirectionGenerateState =
                new RandomDirectionGenerateState(moveDirection, rotationDirection, 0.5f);

            EmptyState emptyState = new EmptyState();

            TimerService movementTimer = _timerServiceFactory.Create(2);
            disposables.Add(randomDirectionGenerateState.Entered.Subscribe(movementTimer.Restart));
            FuncCondition movementTimerEndedCondition = new FuncCondition(() => movementTimer.IsOver);

            TimerService idleTimer = _timerServiceFactory.Create(3);
            disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));
            FuncCondition idleTimerEndedCondition = new FuncCondition(() => idleTimer.IsOver);

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(randomDirectionGenerateState);
            stateMachine.AddState(emptyState);

            stateMachine.AddTransition(randomDirectionGenerateState, emptyState, movementTimerEndedCondition);
            stateMachine.AddTransition(emptyState, randomDirectionGenerateState, idleTimerEndedCondition);

            return stateMachine;
        }
    }
}

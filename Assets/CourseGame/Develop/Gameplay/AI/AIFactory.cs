using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.Timer;
using Assets.CourseGame.Develop.Gameplay.AI.States;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Conditions;
using System;
using System.Collections.Generic;

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

        public AIStateMachine CreateGhostBehaviour(Entity entity) 
            => CreateRandomMovementStateMachine(entity);

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

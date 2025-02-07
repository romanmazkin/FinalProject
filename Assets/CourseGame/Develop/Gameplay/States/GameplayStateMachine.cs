using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class GameplayStateMachine : StateMachine<IUpdatableState>
    {
        public GameplayStateMachine(List<IDisposable> disposables = null) : base(disposables) { }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            CurrentState.State.Update(deltaTime);
        }
    }
}

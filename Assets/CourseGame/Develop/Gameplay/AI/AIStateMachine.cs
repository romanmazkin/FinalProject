using Assets.CourseGame.Develop.Utils.StateMachineBase;
using System;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.AI
{
    public class AIStateMachine : StateMachine<IUpdatableState>
    {
        public AIStateMachine(List<IDisposable> disposables = null) : base(disposables)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            CurrentState.State.Update(deltaTime);
        }
    }
}

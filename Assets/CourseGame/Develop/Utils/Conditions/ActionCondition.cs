using Assets.CourseGame.Develop.Utils.Reactive;
using System;

namespace Assets.CourseGame.Develop.Utils.Conditions
{
    public class ActionCondition : ICondition, IDisposable
    {
        private IReadOnlyEvent _action;

        private bool _isComplete;

        private IDisposable _disposableAction;

        public ActionCondition(IReadOnlyEvent action)
        {
            _action = action;

            _disposableAction = action.Subscribe(OnActionEvent);
        }

        private void OnActionEvent()
        {
            _isComplete = true;
        }

        public bool Evaluate()
        {
            bool temp = _isComplete;
            _isComplete = false;
            return temp;
        }

        public void Dispose()
        {
            _disposableAction.Dispose();
        }
    }
}

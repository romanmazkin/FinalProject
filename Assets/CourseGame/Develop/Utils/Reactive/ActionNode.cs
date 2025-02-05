using System;
namespace Assets.CourseGame.Develop.Utils.Reactive
{
    public class ActionNode : IDisposable
    {
        private Action _action;
        private Action<ActionNode> _onDispose;

        public ActionNode(Action action, Action<ActionNode> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }

        public void Invoke() => _action?.Invoke();

        public void Dispose() => _onDispose?.Invoke(this);
    }

    public class ActionNode<T> : IDisposable
    {
        private Action<T> _action;

        private Action<ActionNode<T>> _onDispose;

        public ActionNode(Action<T> action, Action<ActionNode<T>> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }

        public void Invoke(T arg) => _action?.Invoke(arg);

        public void Dispose() => _onDispose?.Invoke(this);
    }
}
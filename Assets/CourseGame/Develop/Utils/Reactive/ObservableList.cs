using System;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Utils.Reactive
{
    public class ObservableList<T>
    {
        public event Action<T> Added;
        public event Action<T> Removed;

        private List<T> _elements = new();

        public IReadOnlyList<T> Elements => _elements;

        public virtual void Add(T element)
        {
            _elements.Add(element);
            Added?.Invoke(element);
        }

        public virtual void Remove(T element)
        {
            _elements.Remove(element);
            Removed?.Invoke(element);
        }

        protected void Clear() => _elements.Clear();
    }
}

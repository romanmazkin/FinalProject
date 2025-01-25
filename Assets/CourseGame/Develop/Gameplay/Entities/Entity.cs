using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public class Entity : MonoBehaviour
    {
        private readonly Dictionary<EntityValues, object> _values = new();

        private readonly HashSet<IEntityBehaviour> _behaviours = new();

        private readonly List<IEntityUpdate> _updatables = new();
        private readonly List<IEntityInitialize> _initializables = new();
        private readonly List<IEntityDispose> _disposables = new();

        private bool _isInit;

        public void Initialize()
        {
            foreach (IEntityInitialize initializable in _initializables)
                initializable.OnInit(this);

            _isInit = true;
        }

        private void Update()
        {
            if (_isInit == false)
                throw new InvalidOperationException("not inited");

            foreach (IEntityUpdate updatable in _updatables)
                updatable.OnUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            foreach (IEntityDispose disposable in _disposables)
                disposable.OnDispose();
        }

        public Entity AddValue<TValue>(EntityValues valueType, TValue value)
        {
            if (_values.ContainsKey(valueType))
                throw new ArgumentException(valueType.ToString());

            _values.Add(valueType, value);
            return this;
        }

        public bool TryGetValue<TValue>(EntityValues valueType, out TValue value)
        {
            if (_values.TryGetValue(valueType, out object findedObject))
            {
                if (findedObject is TValue findedValue)
                {
                    value = findedValue;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        public TValue GetValue<TValue>(EntityValues valueType)
        {
            if (TryGetValue(valueType, out TValue value) == false)
                throw new ArgumentException($"Entity not exist {valueType}");

            return value;
        }

        public Entity AddBehaviour(IEntityBehaviour behaviour)
        {
            if (_behaviours.Contains(behaviour))
                throw new ArgumentException(behaviour.GetType().ToString());

            _behaviours.Add(behaviour);

            if (behaviour is IEntityUpdate updatable)
                _updatables.Add(updatable);

            if (behaviour is IEntityInitialize initializable)
            {
                _initializables.Add(initializable);

                if (_isInit)
                    initializable.OnInit(this);
            }

            if (behaviour is IEntityDispose disposable)
                _disposables.Add(disposable);

            return this;
        }
    }
}

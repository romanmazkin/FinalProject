using Assets.CourseGame.Develop.Utils.Reactive;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.AI.Sensors
{
    [RequireComponent(typeof(Collider))]
    public class TriggerReciever : MonoBehaviour
    {
        [SerializeField] private List<Collider> _ignoreColliders;

        private ReactiveEvent<Collider> _enter = new();
        private ReactiveEvent<Collider> _exit = new();
        private ReactiveEvent<Collider> _stay = new();

        public IReadOnlyEvent<Collider> Enter => _enter;
        public IReadOnlyEvent<Collider> Exit => _exit;
        public IReadOnlyEvent<Collider> Stay => _stay;

        private void Awake()
        {
            Collider selfCollider = GetComponent<Collider>();

            foreach(Collider collider in _ignoreColliders)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _enter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _exit.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            _stay.Invoke(other);
        }
    }
}
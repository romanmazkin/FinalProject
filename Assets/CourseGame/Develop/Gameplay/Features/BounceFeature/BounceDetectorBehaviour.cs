using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.BounceFeature
{
    public class BounceDetectorBehaviour : IEntityInitialize, IEntityUpdate, IEntityDispose
    {
        private LayerMask _layerToBounceReaction;
        private ReactiveEvent<RaycastHit> _bounceEvent;
        private Transform _transform;
        private TriggerReciever _selfTriggerReciever;

        private Vector3 _previousPosition;
        private Collider _previousObject;

        private IDisposable _triggerStayDisposable;

        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _layerToBounceReaction = entity.GetLayerToBounceReaction();
            _bounceEvent = entity.GetBounceEvent();
            _selfTriggerReciever = entity.GetSelfTriggerReciever();

            _triggerStayDisposable = _selfTriggerReciever.Stay.Subscribe(OnSelfTriggerStay);

            _previousPosition = _transform.position;
        }

        private void OnSelfTriggerStay(Collider anotherObject)
        {
            if (_previousObject == anotherObject)
                return;

            if (Physics.Raycast(_previousPosition, _transform.forward, out RaycastHit hit, 1000, _layerToBounceReaction.value))
            {
                if(hit.collider == anotherObject)
                {
                    _previousObject = anotherObject;
                    _bounceEvent.Invoke(hit);
                    Debug.DrawLine(_previousPosition, _transform.position, Color.red);
                    return;
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            _previousPosition = _transform.position;
        }

        public void OnDispose()
        {
            _triggerStayDisposable.Dispose();
        }
    }
}

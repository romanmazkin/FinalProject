using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Extensions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.DeathFeature
{
    public class SelfTriggerDeathLayerTouchDetector : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<bool> _isTouchDeathLayer;
        private LayerMask _deathLayer;
        private TriggerReciever _selfTriggerReciever;

        private int _enteredCounter;

        private IDisposable _enterDisposable;
        private IDisposable _exitDisposable;

        public void OnInit(Entity entity)
        {
            _selfTriggerReciever = entity.GetSelfTriggerReciever();
            _isTouchDeathLayer = entity.GetIsTouchDeathLayer();
            _deathLayer = entity.GetDeathLayer();

            _enterDisposable = _selfTriggerReciever.Enter.Subscribe(OnSelfTriggerEnter);
            _exitDisposable = _selfTriggerReciever.Exit.Subscribe(OnSelfTriggerExit);
        }

        private void OnSelfTriggerExit(Collider other)
        {
            if (other.MatchWithLayer(_deathLayer))
            {
                _enteredCounter--;

                if (_enteredCounter == 0)
                    _isTouchDeathLayer.Value = false;
            }
        }

        private void OnSelfTriggerEnter(Collider other)
        {
            if (other.MatchWithLayer(_deathLayer))
            {
                _enteredCounter++;
                _isTouchDeathLayer.Value = true;
            }
        }

        public void OnDispose()
        {
            _enterDisposable.Dispose();
            _exitDisposable.Dispose();
        }
    }

    public class SelfTriggerTouchAnotherTeamDetector : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<bool> _isTouchAnotherTeam;
        private TriggerReciever _selfTriggerReciever;
        private ReactiveVariable<int> _sourceTeam;

        private int _enteredCounter;

        private IDisposable _enterDisposable;
        private IDisposable _exitDisposable;

        public void OnInit(Entity entity)
        {
            _isTouchAnotherTeam = entity.GetIsTouchAnotherTeam();
            _selfTriggerReciever = entity.GetSelfTriggerReciever();
            _sourceTeam = entity.GetTeam();

            _enterDisposable = _selfTriggerReciever.Enter.Subscribe(OnSelfTriggerEnter);
            _exitDisposable = _selfTriggerReciever.Exit.Subscribe(OnSelfTriggerExit);
        }

        private void OnSelfTriggerExit(Collider other)
        {
            if (other.TryGetEntity(out Entity entity))
            {
                if (entity.HasTeam() == false)
                    return;

                if (entity.MathWithTeam(_sourceTeam.Value) == false)
                {
                    _enteredCounter--;

                    if(_enteredCounter == 0)
                        _isTouchAnotherTeam.Value = false;
                }
            }
        }

        private void OnSelfTriggerEnter(Collider other)
        {
            if(other.TryGetEntity(out Entity entity))
            {
                if (entity.HasTeam() == false)
                    return;

                if(entity.MathWithTeam(_sourceTeam.Value) == false)
                {
                    _enteredCounter++;
                    _isTouchAnotherTeam.Value = true;
                }
            }
        }

        public void OnDispose()
        {
            _enterDisposable.Dispose();
            _exitDisposable.Dispose();
        }
    }
}

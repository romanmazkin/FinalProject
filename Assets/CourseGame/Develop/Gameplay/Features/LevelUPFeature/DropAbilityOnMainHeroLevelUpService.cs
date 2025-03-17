using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Presenters;
using Assets.CourseGame.Develop.Gameplay.Features.MainHeroFeature;
using Assets.CourseGame.Develop.Gameplay.Features.PauseFeature;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.LevelUPFeature
{
    public class DropAbilityOnMainHeroLevelUpService : IDisposable
    {
        private MainHeroHolderService _mainHeroHolderService;
        private AbilityPresentersFactory _abilityPresentersFactory;
        private IPauseService _pauseService;
        private ICoroutinePerformer _coroutinePerformer;

        private Queue<int> _levelUpRequests = new();

        private AbilitySelectPopupPresenter _popup;
        private Coroutine _selectAbilityProcess;

        private IDisposable _heroRegistredDisposable;
        private IDisposable _heroUnregistredDisposable;

        public DropAbilityOnMainHeroLevelUpService(
            MainHeroHolderService mainHeroHolderService,
            AbilityPresentersFactory abilityPresentersFactory,
            IPauseService pauseService,
            ICoroutinePerformer coroutinePerformer)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _abilityPresentersFactory = abilityPresentersFactory;
            _pauseService = pauseService;
            _coroutinePerformer = coroutinePerformer;

            _heroRegistredDisposable = _mainHeroHolderService.HeroRegistred.Subscribe(OnMainHeroRegistred);
            _heroUnregistredDisposable = _mainHeroHolderService.HeroUnregistred.Subscribe(OnMainHeroUnregistred);
        }

        private bool PopupIsOpened => _popup != null;

        private void OnMainHeroUnregistred(Entity hero) => hero.GetLevel().Changed -= OnHeroLevelChanged;

        private void OnMainHeroRegistred(Entity hero) => hero.GetLevel().Changed += OnHeroLevelChanged;

        private void OnHeroLevelChanged(int arg1, int currentLevel)
        {
            _levelUpRequests.Enqueue(currentLevel);

            if (_selectAbilityProcess != null)
                return;

            _selectAbilityProcess = _coroutinePerformer.StartPerform(SelectAbilityProcess());
        }

        private IEnumerator SelectAbilityProcess()
        {
            while (_levelUpRequests.Count > 0)
            {
                int level = _levelUpRequests.Dequeue();

                _pauseService.Pause();
                _popup = _abilityPresentersFactory.CreateAbilitySelectPopupPresenter(_mainHeroHolderService.MainHero);
                _popup.Enable();

                _popup.CloseRequest += OnAbilitySelected;

                yield return new WaitUntil(() => PopupIsOpened == false);
            }

            _selectAbilityProcess = null;
        }

        private void OnAbilitySelected(AbilitySelectPopupPresenter popup)
        {
            popup.CloseRequest -= OnAbilitySelected;

            _popup.Disable(() =>
            {
                _pauseService.Unpause();
                _popup = null;
            });
        }

        public void Dispose()
        {
            _heroRegistredDisposable.Dispose();
            _heroUnregistredDisposable.Dispose();
        }
    }
}

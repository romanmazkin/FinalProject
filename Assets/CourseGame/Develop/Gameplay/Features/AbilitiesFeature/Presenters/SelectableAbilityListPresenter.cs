using Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.AbilityDropServiceFeature;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Presenters
{
    public class SelectableAbilityListPresenter
    {
        public event Action ProvideComplete;

        private const int AbilitiesCount = 3;

        private SelectableAbilityListView _view;
        private Entity _entity;
        private AbilityDropService _abilityDropper;
        private AbilityPresentersFactory _abilityPresentersFactory;

        private List<SelectableAbilityPresenter> _presenters = new();
        private SelectableAbilityPresenter _selectedPresenter;

        public SelectableAbilityListPresenter(
            SelectableAbilityListView selectableAbilityListView,
            Entity entity,
            AbilityDropService abilityDropper,
            AbilityPresentersFactory abilityPresentersFactory)
        {
            _view = selectableAbilityListView;
            _entity = entity;
            _abilityDropper = abilityDropper;
            _abilityPresentersFactory = abilityPresentersFactory;
        }

        public void Enable()
        {
            _view.SelectButtonClicked += OnSelectButtonClicked;

            List<AbilityDropOption> dropOptions = _abilityDropper.Drop(AbilitiesCount, _entity);

            for(int i = 0; i < dropOptions.Count; i++)
            {
                SelectableAbilityView selectableAbilityView = _view.SpawnItem();

                SelectableAbilityPresenter presenter = _abilityPresentersFactory
                    .CreateSelectableAbilityPresenter(dropOptions[i].Config, selectableAbilityView, _entity, dropOptions[i].Level);

                presenter.Selected += OnPresenterSelected;

                presenter.Enable();
                _presenters.Add(presenter);
            }
        }

        public void Disable()
        {
            foreach (var presenter in _presenters)
            {
                _view.Remove(presenter.View);
                presenter.Selected -= OnPresenterSelected;
                presenter.Disable();
            }

            _presenters.Clear();
            _view.SelectButtonClicked -= OnSelectButtonClicked;
        }

        public void EnableSubscribes()
        {
            _view.Subscribe();

            foreach (var presenter in _presenters)
                presenter.EnableSubscribes();
        }

        public void DisableSubscribes()
        {
            _view.Unsubscribe();

            foreach (var presenter in _presenters)
                presenter.DisableSubscribes();
        }

        private void OnSelectButtonClicked()
        {
            _selectedPresenter.Provide();
            ProvideComplete?.Invoke();
        }

        private void OnPresenterSelected(SelectableAbilityPresenter selected)
        {
            _view.Select(selected.View);
            _selectedPresenter = selected;
        }
    }
}

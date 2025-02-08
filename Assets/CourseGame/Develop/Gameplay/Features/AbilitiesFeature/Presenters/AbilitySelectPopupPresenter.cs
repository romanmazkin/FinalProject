using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Presenters
{
    public class AbilitySelectPopupPresenter
    {
        public event Action<AbilitySelectPopupPresenter> CloseRequest;

        private const string Title = "LEVEL {0} IN THIS ADVENTURE";
        private const string SelectAbilityText = "Select ability";

        private AbilitySelectPopupView _view;
        private SelectableAbilityListPresenter _selectableAbilityListPresenter;

        private Entity _entity;
        private AbilityPresentersFactory _presentersFactory;

        private ICoroutinePerformer _coroutinePerformer;

        public AbilitySelectPopupPresenter(
            AbilitySelectPopupView view, 
            Entity entity,
            ICoroutinePerformer coroutinePerformer,
            AbilityPresentersFactory presentersFactory)
        {
            _view = view;
            _entity = entity;
            _coroutinePerformer = coroutinePerformer;
            _presentersFactory = presentersFactory;
        }

        public void Enable()
        {
            _view.SetTitle(string.Format(Title, 2));
            _view.SetAdditionalText(SelectAbilityText);

            _selectableAbilityListPresenter = _presentersFactory.CreateSelectableAbilityListPresenter(_view.AbilityListView, _entity);

            _selectableAbilityListPresenter.Enable();

            _selectableAbilityListPresenter.ProvideComplete += OnProvideComplete;

            _coroutinePerformer.StartPerform(Show());
        }

        public void Disable(Action callback)
        {
            _selectableAbilityListPresenter.ProvideComplete -= OnProvideComplete;

            _coroutinePerformer.StartPerform(Hide(callback));
        }

        private IEnumerator Hide(Action callback)
        {
            _selectableAbilityListPresenter.Disable();
            yield return _view.Hide();

            _selectableAbilityListPresenter.Disable();

            Object.Destroy(_view.gameObject);

            callback?.Invoke();
        }

        private IEnumerator Show()
        {
            yield return _view.Show();

            _selectableAbilityListPresenter.EnableSubscribes();
        }

        private void OnProvideComplete() => CloseRequest?.Invoke(this);
    }
}

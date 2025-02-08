using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.CommonServices.CoroutinePerformer;
using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.AbilityDropServiceFeature;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View;
using Assets.CourseGame.Develop.Gameplay.UI;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Presenters
{
    public class AbilityPresentersFactory
    {
        private DIContainer _container;

        public AbilityPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public SelectableAbilityPresenter CreateSelectableAbilityPresenter(
            AbilityConfig abilityConfig, SelectableAbilityView view, Entity entity, int level)
        {
            return new SelectableAbilityPresenter(abilityConfig, view, _container.Resolve<AbilityFactory>(), entity, level);
        }

        public SelectableAbilityListPresenter CreateSelectableAbilityListPresenter(
            SelectableAbilityListView view, Entity entity)
        {
            return new SelectableAbilityListPresenter(view, entity, _container.Resolve<AbilityDropService>(), this);
        }

        public AbilitySelectPopupPresenter CreateAbilitySelectPopupPresenter(Entity entity)
        {
            AbilitySelectPopupView viewPrefab = _container.Resolve<ResourcesAssetLoader>().LoadResource<AbilitySelectPopupView>("Gameplay/UI/Abilities/SelectionAbilityPopup");
            AbilitySelectPopupView view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().PopupsLayer);
            return new AbilitySelectPopupPresenter(view, entity, _container.Resolve<ICoroutinePerformer>(), this);
        }
    }
}

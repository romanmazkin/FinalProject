using Assets.CourseGame.Develop.Configs.Gameplay.Abilities;
using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.View;
using Assets.CourseGame.Develop.Utils.Extensions;
using System;
using System.Linq;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.Presenters
{
    public class SelectableAbilityPresenter
    {
        public event Action<SelectableAbilityPresenter> Selected;

        private AbilityFactory _abilityFactory;
        private Entity _entity;

        private int _level;

        public SelectableAbilityPresenter(
           AbilityConfig abilityConfig,
           SelectableAbilityView view,
           AbilityFactory abilityFactory,
           Entity entity,
           int level)
        {
            AbilityConfig = abilityConfig;
            View = view;
            _abilityFactory = abilityFactory;
            _entity = entity;
            _level = level;
        }

        public AbilityConfig AbilityConfig { get; }
        public SelectableAbilityView View { get; }

        public void Enable()
        {
            View.SetName(AbilityConfig.Name);
            View.SetDescription(AbilityConfig.Description);
            View.Icon.SetIcon(AbilityConfig.Icon);

            InitByAbilityList();

            View.Clicked += OnViewClicked;
        }

        public void Disable()
        {
            View.Clicked -= OnViewClicked;
        }

        public void Provide()
        {
            Ability ability;

            if (AbilityConfig.IsUpgradable())
            {
                ability = _entity.GetAbilityList().Elements.FirstOrDefault(abil => abil.ID == AbilityConfig.ID);

                if(ability != null)
                {
                    ability.AddLevel(_level);
                    return;
                }
            }

            ability = _abilityFactory.CreateAbilityFor(_entity, AbilityConfig, _level);
            _entity.GetAbilityList().Add(ability);
        }

        public void EnableSubscribes() => View.Subscribe();

        public void DisableSubscribes() => View.Unsubscribe();  

        private void OnViewClicked() => Selected?.Invoke(this);

        private void InitByAbilityList()
        {
            if (AbilityConfig.IsUpgradable())
            {
                Ability ability = _entity.GetAbilityList().Elements.FirstOrDefault(abil => abil.ID == AbilityConfig.ID);

                if(ability != null)
                {
                    View.Icon.ShowLevel();
                    View.Icon.SetLevel("LV." + ability.CurrentLevel.Value);
                    View.SetTabletText("LV." + ability.CurrentLevel.Value + "->" + "LV." + (ability.CurrentLevel.Value + _level));
                }
                else
                {
                    View.Icon.HideLevel();
                    View.SetTabletText("NEW LV." + _level);
                }
            }
            else
            {
                View.Icon.HideLevel();
                View.SetTabletText("NEW");
            }
        }
    }
}

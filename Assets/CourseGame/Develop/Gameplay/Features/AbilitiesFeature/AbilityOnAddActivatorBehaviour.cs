using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature
{
    public class AbilityOnAddActivatorBehaviour : IEntityInitialize, IEntityDispose
    {
        private AbilityList _abilityList;

        public void OnInit(Entity entity)
        {
            _abilityList = entity.GetAbilityList();

            _abilityList.Added += OnAbilityAdded;

            foreach (Ability ability in _abilityList.Elements)
                ability.Activate();
        }

        private void OnAbilityAdded(Ability ability)
        {
            ability.Activate();
        }

        public void OnDispose()
        {
            _abilityList.Added -= OnAbilityAdded;
        }
    }
}

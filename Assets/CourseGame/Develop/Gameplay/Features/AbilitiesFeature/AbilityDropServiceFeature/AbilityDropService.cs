using Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions;
using Assets.CourseGame.Develop.Gameplay.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature.AbilityDropServiceFeature
{
    public class AbilityDropService
    {
        private AbilityDropOptionsConfig _abilityDropOptionsConfig;
        private AbilityDropingRules _abilityDropingRules;

        public AbilityDropService(
            AbilityDropOptionsConfig abilityDropOptionsConfig, 
            AbilityDropingRules abilityDropingRules)
        {
            _abilityDropOptionsConfig = abilityDropOptionsConfig;
            _abilityDropingRules = abilityDropingRules;
        }

        public List<AbilityDropOption> Drop(int count, Entity entity)
        {
            List<AbilityDropOption> availablesOptions 
                = new List<AbilityDropOption>(_abilityDropOptionsConfig.DropOptions
                .Where(dropOption => _abilityDropingRules.IsAvailable(dropOption, entity)));

            List<AbilityDropOption> selectedOptions = new();

            for (int i = 0; i < count; i++)
            {
                AbilityDropOption selectedOption = availablesOptions[UnityEngine.Random.Range(0, availablesOptions.Count)];
                selectedOptions.Add(selectedOption);
                availablesOptions.Remove(selectedOption);
            }

            return selectedOptions;
        }
    }
}

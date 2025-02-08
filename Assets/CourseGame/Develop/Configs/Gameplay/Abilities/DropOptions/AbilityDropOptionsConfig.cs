using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/AbilityDtopOptionsConfig", fileName = "AbilityDtopOptionsConfig")]
    public class AbilityDropOptionsConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityDropOption> _dropOptions;

        public IReadOnlyList<AbilityDropOption> DropOptions => _dropOptions;
    }
}

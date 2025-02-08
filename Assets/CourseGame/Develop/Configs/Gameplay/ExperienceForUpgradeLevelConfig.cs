using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/ExperienceForUpgradeLevelConfig", fileName = "ExperienceForUpgradeLevelConfig")]
    public class ExperienceForUpgradeLevelConfig : ScriptableObject
    {
        [SerializeField] private List<float> _experienceForLevel;

        public int MaxLevel => _experienceForLevel.Count;
        public float GetExperienceFor(int level) => _experienceForLevel[level - 1];
    }
}

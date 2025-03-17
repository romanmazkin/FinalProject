using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/HealthLootConfig", fileName = "HealthLootConfig")]
    public class HealthLootConfig : LootConfig
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}

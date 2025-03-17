using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/GoldLootConfig", fileName = "GoldLootConfig")]
    public class CoinsLootConfig : LootConfig
    {
        [field: SerializeField] public int Coins { get; private set; }
    }
}

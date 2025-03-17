using Assets.CourseGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Loot
{
    public abstract class LootConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Entity Prefab { get; private set; }
    }
}

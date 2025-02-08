using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions
{
    [Serializable]
    public class AbilityDropOption
    {
        [field: SerializeField] public AbilityConfig Config { get; private set ;}
        [field: SerializeField] public int Level { get; private set; } = 1;
        //шанс выпадения, уровень выпадающей спобности
    }
}

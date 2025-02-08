using System;
using UnityEngine;

namespace Assets.CourseGame.Develop.Configs.Gameplay.Abilities.DropOptions
{
    [Serializable]
    public class AbilityDropOption
    {
        [field: SerializeField] public AbilityConfig Config { get; private set ;}
        //шанс выпадения, уровень выпадающей спобности
    }
}

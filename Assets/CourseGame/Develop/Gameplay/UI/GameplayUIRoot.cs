using Assets.CourseGame.Develop.CommonUI;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI
{
    public class GameplayUIRoot : MonoBehaviour
    {
        [field: SerializeField] public Joystick Joystick { get; private set; }
        [field: SerializeField] public IconWithText CoinsView { get; private set; }
        [field: SerializeField] public CoinsAddedEffectView CoinsAddedEffectView { get; private set; }
        [field: SerializeField] public Transform HUDLayer { get; private set; }
        [field: SerializeField] public Transform PopupsLayer { get; private set; }
        [field: SerializeField] public Transform VFXLayer { get; private set; }
    }
}

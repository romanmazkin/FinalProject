using Assets.CourseGame.Develop.CommonUI;
using UnityEngine;

public class MainMenuUIRoot : MonoBehaviour
{
    [field: SerializeField] public IconWithText _currencyView;
    [field: SerializeField] public Transform HUDLayer {  get; private set; }
    [field: SerializeField] public Transform PopupsLayer {  get; private set; }
    [field: SerializeField] public Transform VFXLayer {  get; private set; }
}

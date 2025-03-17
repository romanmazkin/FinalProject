using Assets.CourseGame.Develop.CommonUI;
using UnityEngine;

public class MainMenuUIRoot : MonoBehaviour
{
    [field: SerializeField] public IconWithTextListView WalletView {  get; private set; }
    [field: SerializeField] public ActionButton OpenLevelsMenuButton {  get; private set; }
    [field: SerializeField] public ActionButton OpenStatsUpgradePopupButton { get; private set; }
    [field: SerializeField] public Transform HUDLayer {  get; private set; }
    [field: SerializeField] public Transform PopupsLayer {  get; private set; }
    [field: SerializeField] public Transform VFXLayer {  get; private set; }
}

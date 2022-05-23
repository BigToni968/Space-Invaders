using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/GUI Data", fileName = "GUI Data", order = 1)]
public class GUIData : ScriptableObject
{
    [Header("Prefabs GUI")]
    public GUIElement[] GUIElements = null;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Space Ship Data/Default", fileName = "SpaceShipData", order = 1)]
public class SpaceShipData : ScriptableObject
{
    public int Life = 1;
    public float Speed = 1;
    [Header("Space Ship prefab")]
    public SpaceShip SpaceShipModel = null;
}

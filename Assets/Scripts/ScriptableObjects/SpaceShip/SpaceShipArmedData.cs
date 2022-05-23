using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Space Ship Data/Armed", fileName = "ArmedData", order = 1)]
public class SpaceShipArmedData : SpaceShipUnarmedData
{
    [Header("Delay before attack")]
    public float DelayAttack = 1f;
}

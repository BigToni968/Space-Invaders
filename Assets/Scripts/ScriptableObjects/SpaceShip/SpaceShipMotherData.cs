using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Space Ship Data/Mother", fileName = "MotherData", order = 1)]
public class SpaceShipMotherData : SpaceShipArmedData
{
    [Header("Delay Before Maneuver")]
    public float DelayManeuver = 1f;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Space Ship Data/Unarmed", fileName = "UnarmedData", order = 1)]
public class SpaceShipUnarmedData : SpaceShipData
{
    [Header("Points for destroying this ship")]
    public float DeadPoints = 1f;
    [Header("Acceleration SpaceShip")]
    public float Acceleration = 1f;
    [Header("Step the distance forward")]
    public float DistanceForward = 1f;
    [Header("Delay to step")]
    public float DelayStep = 1f;
    [Header("Default direction")]
    public EnumDirection DefaultDirection = EnumDirection.Zero;
    [Header("Step in that direction")]
    public EnumDirection StepDirection = EnumDirection.Zero;
}

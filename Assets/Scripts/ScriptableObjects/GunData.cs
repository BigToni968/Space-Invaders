using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Gun Data", fileName = "GunData", order = 1)]
public class GunData : ScriptableObject
{
    public float Damage = 1f;
    public float TimeLife = 1f;
    [Header("SnapShot prefab")]
    public SnapShot SnapShot = null;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Wave Data", fileName = "WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    // Number of ships maybe?
    [Header("Number of controllers")]
    public int Count = 0;
    [Header("Controller prefab")]
    public UnarmedController Controller = null;
}

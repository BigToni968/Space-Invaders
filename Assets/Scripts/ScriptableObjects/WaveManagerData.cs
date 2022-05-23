using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Wave Manager Data", fileName = "WaveManagerData", order = 1)]
public class WaveManagerData : ScriptableObject
{
    [Header("The disatance between waves")]
    public float DistanceBetween = 1f;
    [Header("Prefabs waves")]
    public List<WaveUnarmed> Waves = null;
    [Header("Mode for waves of ships")]
    public ModeInstantiateWave ModeInstantiateWave = ModeInstantiateWave.All;
}

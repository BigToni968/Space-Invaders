using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Game Manager Data", fileName = "GameManagerData", order = 1)]
public class GameManagerData : ScriptableObject
{
    [Header("Countdown to the start of the game")]
    public int Countdown = 3;
    [Header("Prefabs that are needed for gameplay")]
    public GameObject[] GameObjects = null;
}

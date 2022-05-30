using UnityEngine;

[CreateAssetMenu(menuName = "Space Invaders/Game Manager Data", fileName = "GameManagerData", order = 1)]
public class GameManagerData : ScriptableObject
{
    [Header("Countdown to the start of the game")]
    public int Countdown = 3;
    
    // I like how you go for flexibility in terms of which prefabs to load,
    // But I think in this case it does more harm than good because it forces you to use
    // GetComponent and FindObject later on
    [Header("Prefabs that are needed for gameplay")]
    public GameObject[] GameObjects = null;
}

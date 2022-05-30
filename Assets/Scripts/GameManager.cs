using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerData _gameManagerData = null;

    // Why use "protected" access modifier when this class has no inheritors?
    // this should have been "private"
    public GUI GUI { get; protected set; }
    public PlayerController PlayerController { get; protected set; }
    public WaveManager WaveManager { get; protected set; }
    public Countdown Countdown { get; protected set; }
    public Camera Camera { get; protected set; }
    public bool Pause { get; protected set; } = true;

    private void Awake()
    {
        Instantiate();
        GUI = GetComponentInChildren<GUI>();
        PlayerController = GetComponentInChildren<PlayerController>();
        WaveManager = GetComponentInChildren<WaveManager>();
        Countdown = GetComponentInChildren<Countdown>();
        Camera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        StartCoroutine(PlayerPreparationBeforeThisGame());
    }

    private void OnEnable()
    {
        PlayerController.DestroyedConttroller += LosePlayer;
        WaveManager.CleanWaves += CleanedUpWaves;
    }

    private void OnDisable()
    {
        PlayerController.DestroyedConttroller -= LosePlayer;
        WaveManager.CleanWaves -= CleanedUpWaves;
    }

    private void OnDestroy() => OnDisable();

    private void Instantiate()
    {
        if (_gameManagerData == null)
        {
            Debug.Log("No data found for the game manager");
            return;
        }

        // This seems a bit overengineered. Since there is only one type of GameManager,
        // we could have saved all prefabs into it's fields with [SerializeField] and then instantiated them.
        for (int i = 0; i < _gameManagerData.GameObjects.Length; i++)
            Instantiate(_gameManagerData.GameObjects[i], transform);
    }

    private void LosePlayer()
    {
        GUI.CallNotification(TypeNotification.Selection,
            "Too bad, but you lost. Do you wnt to start over or" +
            "go back to the main menu?", "Restart",
            "Main menu").CallBackInterception += ChoiseAter;
        Pause = true;
    }

    private void CleanedUpWaves()
    {
        GUI.CallNotification(TypeNotification.Selection,
            "Congratulations! You managed to defend the invasion of the space" +
            "invaders. Do you want to repeat the feat or would you to return to the main menu? ", "Restart",
            "Main menu").CallBackInterception += ChoiseAter;
        Pause = true;
    }

    private void ChoiseAter(ResultNotification result)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (result == ResultNotification.Cancel) currentScene--;
        SceneManager.LoadScene(currentScene);
    }

    private IEnumerator PlayerPreparationBeforeThisGame()
    {
        PlayerController.transform.position =
            new Vector2(Camera.transform.position.x - Camera.Rectangle().Widht,
            Camera.transform.position.y - Camera.Rectangle().Height);

        Vector2 target = new Vector2(PlayerController.transform.position.x + Camera.Rectangle().Widht,
            PlayerController.transform.position.y + Camera.Rectangle().Height);

        Countdown.Run(_gameManagerData.Countdown);
        Countdown.CountdownEnd += delegate
        {
            GUI.CallNotification(TypeNotification.Information, "Warning. Space invaders are on the horizon." +
              "\nBefore you fight back, there are a few things you should know:\n" +
              "1) Move on the arrows.\n" +
              "2) Fire on the space key", "It all makes sense.").CallBackInterception
              += delegate { Pause = false; };
        };

        while (Pause)
        {
            PlayerController.transform.position = Vector2.Lerp(PlayerController.transform.position, target, Time.deltaTime);
            yield return null;
        }
    }

    private void Update()
    {
        if (Pause)
            return;

        WaveManager.OnUpdate();
        PlayerController.SpaceShip.OnUpdate();
    }
}

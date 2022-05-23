using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : GUIElement
{
    [SerializeField] private Button[] _menuButtons = null;
    private GUI _gUI = null;
    private void Awake()
    {
        _menuButtons = _menuButtons.Length == 0 ? GetComponentsInChildren<Button>() : _menuButtons;
        AddListenerMenuButtons();
        _gUI = FindObjectOfType<GUI>();
    }

    private void AddListenerMenuButtons()
    {
        foreach (Button menuButton in _menuButtons)
            menuButton.onClick.AddListener(() => OnClickListener(menuButton));
    }

    private void OnClickListener(Button MenuButton)
    {
        switch (MenuButton.name.ToLower().Trim())
        {
            case "play":
                ChekingResult(ResultNotification.Ok);
                break;
            case "continue":
                _gUI.
                    CallNotification(TypeNotification.Selection, "You do not have a saved result. Choosing to continue now will only start the game.").
                    CallBackInterception += ChekingResult;
                break;
            case "exit":
                Application.Quit();
                break;
        }
    }

    private void ChekingResult(ResultNotification result)
    {
        if (result == ResultNotification.Ok)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

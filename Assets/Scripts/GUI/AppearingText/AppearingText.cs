using UnityEngine;
using TMPro;

public class AppearingText : GUIElement
{
    [SerializeField] private TextMeshProUGUI _outputText = null;
    [SerializeField] private float _timeLife = 2f;

    private void Awake() => _outputText = GetComponentInChildren<TextMeshProUGUI>();

    public void Spawn(Vector2 Position, float Score)
    {
        TextMeshProUGUI textMeshProUGUI = Instantiate(_outputText, transform);
        textMeshProUGUI.text = "+" + Score.ToString();
        textMeshProUGUI.transform.position = Camera.main.WorldToScreenPoint(Position);
        textMeshProUGUI.enabled = true;
        Destroy(textMeshProUGUI.gameObject, _timeLife);
    }
}

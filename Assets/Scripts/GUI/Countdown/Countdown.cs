using System.Collections;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class Countdown : GUIElement
{
    [SerializeField] private TextMeshProUGUI _outputCountdown = null;
    public Action CountdownEnd = delegate { };

    private Canvas _countdownWindow = null;
    private void Awake()
    {
        _countdownWindow = GetComponent<Canvas>();
        if (_outputCountdown == null)
            _outputCountdown = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Run(int Countdown)
    {
        _countdownWindow.enabled = true;
        StartCoroutine(Performed(Countdown));
    }

    private IEnumerator Performed(int Countdown)
    {
        for (int i = Countdown; i > 0; i--)
        {
            if (_outputCountdown != null)
                _outputCountdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        CountdownEnd?.Invoke();
        CountdownEnd = null;
        _countdownWindow.enabled = false;
    }
}

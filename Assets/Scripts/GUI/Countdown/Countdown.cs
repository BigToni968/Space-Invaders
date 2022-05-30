using System.Collections;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class Countdown : GUIElement
{
    [SerializeField] private TextMeshProUGUI _outputCountdown = null;
    
    // Using Nouns is generally not recommended for names of events. Consider using Verbs in past simple, like "CountdownEnded"
    public Action CountdownEnd = delegate { };

    private Canvas _countdownWindow = null;
    private void Awake()
    {
        // Please don't use GetComponent when it's possible to use SerializeField
        _countdownWindow = GetComponent<Canvas>();
        if (_outputCountdown == null)
            _outputCountdown = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Run(int Countdown)
    {
        _countdownWindow.enabled = true;
        StartCoroutine(Performed(Countdown));
    }

    // Using Verbs in past tense is generally not recommended
    // Consider using RunTimer instead
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

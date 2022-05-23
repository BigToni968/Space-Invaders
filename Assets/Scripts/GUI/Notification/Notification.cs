using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Notification : NotificationBase
{
    [SerializeField] protected Canvas _window = null;
    [SerializeField] private Button[] _callBackButtons = null;
    [SerializeField] private TextMeshProUGUI _output = null;

    public override event Action<ResultNotification> CallBackInterception = delegate { };
    protected override string Message { get; set; }
    protected override Button[] CallBackButtons { get => _callBackButtons; set => _callBackButtons = value; }
    protected override TextMeshProUGUI Output { get => _output; set => _output = value; }

    protected TextMeshProUGUI[] _textCallBackButtons = null;

    protected override void Initializing()
    {
        base.Initializing();
        _window = _window == null ? GetComponentInChildren<Canvas>() : _window;
        _callBackButtons = _callBackButtons.Length < 2 ? GetComponentsInChildren<Button>() : _callBackButtons;
        _output = _output == null ? GetComponentInChildren<TextMeshProUGUI>() : _output;

        SetTextCallBackButtons();
        AddListenerCallBackButtons();
    }

    protected virtual void SetTextCallBackButtons()
    {
        _textCallBackButtons = new TextMeshProUGUI[CallBackButtons.Length];
        for (byte i = 0; i < CallBackButtons.Length; i++)
            _textCallBackButtons[i] = CallBackButtons[i].GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void AddListenerCallBackButtons()
    {
        for (byte i = 0; i < CallBackButtons.Length; i++)
        {
            byte index = i;
            CallBackButtons[i].onClick.AddListener(() =>
            {
                CallBack((ResultNotification)index + 1);
                HideMessage();
            });
        }
    }
    protected override void CallBack(ResultNotification Result)
    {
        base.CallBack(Result);
        CallBackInterception?.Invoke(Result);
    }

    public override void Set(string Message, params string[] OutputCallBackButtons)
    {
        base.Set(Message, OutputCallBackButtons);
        Output.text = Message;

        if (OutputCallBackButtons.Length > 0)
            for (byte i = 0; i < _textCallBackButtons.Length; i++)
                if (_textCallBackButtons.Length >= OutputCallBackButtons.Length)
                    _textCallBackButtons[i].text = OutputCallBackButtons[i];
    }

    public override void ShowMessage()
    {
        base.ShowMessage();
        Time.timeScale = Convert.ToSingle(!(_window.enabled = true));
    }

    public override void HideMessage()
    {
        base.HideMessage();
        Time.timeScale = Convert.ToSingle(!(_window.enabled = false));
        CallBackInterception = null;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    [Header("Important field")]
    [SerializeField] private GUIData _gUIData = null;

    public Score Score { get; protected set; }
    public AppearingText AppearingText { get; protected set; }

    private List<GUIElement> _gUIElements = null;

    private void Awake()
    {
        _gUIElements = InstantiateNotification();
        Score = GetComponentInChildren<Score>();
        AppearingText = GetComponentInChildren<AppearingText>();
    }
    protected virtual List<GUIElement> InstantiateNotification()
    {
        List<GUIElement> notifications = new List<GUIElement>(_gUIData.GUIElements.Length);

        for (byte i = 0; i < notifications.Capacity; i++)
            notifications.Add(Instantiate(_gUIData.GUIElements[i], transform));

        return notifications;
    }

    public Notification CallNotification(TypeNotification Type, string Message, params string[] OutputCallbackNotififcationButtons)
    {
        Notification notification = null;

        switch (Type)
        {
            case TypeNotification.Information:
                InformationNotification information = FindTypeNotification<InformationNotification>() as InformationNotification;
                information.Set(Message, OutputCallbackNotififcationButtons);
                information.ShowMessage();
                notification = information;
                break;
            case TypeNotification.Selection:
                SelectionNotification selection = FindTypeNotification<SelectionNotification>() as SelectionNotification;
                selection.Set(Message, OutputCallbackNotififcationButtons);
                selection.ShowMessage();
                notification = selection;
                break;
        }

        return notification;
    }

    private Notification FindTypeNotification<U>()
    {
        for (byte i = 0; i < _gUIElements.Count; i++)
            if (_gUIElements[i] is U) return _gUIElements[i] as Notification;

        return null;
    }
}

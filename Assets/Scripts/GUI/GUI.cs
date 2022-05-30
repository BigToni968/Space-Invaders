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
        // Please don't use GetComponent when it's possible to use [SerializeField]
        Score = GetComponentInChildren<Score>();
        AppearingText = GetComponentInChildren<AppearingText>();
    }
    protected virtual List<GUIElement> InstantiateNotification()
    {
        List<GUIElement> notifications = new List<GUIElement>(_gUIData.GUIElements.Length);

        // There is no need to use byte for i, because when you call _gUIData.GUIElements[i]
        // i will be cast to int anyway, so it's actually slower than just using int
        
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
        // It would have been better to declare guiElements as dictionary
        // private Dictionary<Type, GUIElement> _gUIElements
        //
        // and add elements like so:
        // _gUIElements.Add(element.GetType, element)
        //
        // and extract elements like so:
        // return _gUIEements[typeof(U)];
        //
        // This would have given us O(1) complexity instead of O(n) as in your implementation
        
        
        for (byte i = 0; i < _gUIElements.Count; i++)
            if (_gUIElements[i] is U) return _gUIElements[i] as Notification;

        return null;
    }
}

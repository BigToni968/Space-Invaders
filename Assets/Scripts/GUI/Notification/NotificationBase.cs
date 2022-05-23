using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public abstract class NotificationBase : GUIElement
{
    public abstract event Action<ResultNotification> CallBackInterception;
    protected abstract string Message { get; set; }
    protected abstract Button[] CallBackButtons { get; set; }
    protected abstract TextMeshProUGUI Output { get; set; }
    protected virtual void Initializing() => Debug.Log($"Notiification {this.GetType()} Initializing()");
    public virtual void Set(String Message, params string[] OutputCallBackButtons) => Debug.Log($"Notiification {this.GetType()} Set()");
    public virtual void ShowMessage() => Debug.Log($"Notiification {this.GetType()} ShowMessage({Message})");
    public virtual void HideMessage() => Debug.Log($"Notiification {this.GetType()} HideMessage({Message})");
    protected virtual void CallBack(ResultNotification Result) => Debug.Log($"Notiification {this.GetType()} CallBack({Result})");
}

using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WaveBase : MonoBehaviour
{
    public abstract event Action<WaveBase> WaveIsCleaned;
    protected abstract WaveData WaveData { get; set; }
    protected abstract List<UnarmedController> Controllers { get; set; }

    public virtual void Initializing() => Debug.Log($"Wave {this.GetType()} Initializing()");
    public virtual void Instantiate() => Debug.Log($"Wave {this.GetType()} Instantiate()");

    public virtual void Clear() => Debug.Log($"Wave {this.GetType()} Clear()");

    public virtual void OnUpdate() => Debug.Log($"Wave {this.GetType()} OnUpdate()");
}

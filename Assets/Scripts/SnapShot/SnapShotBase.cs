using UnityEngine;
using System;

// SnapShotBase seems to be not different from SnapShot, not sure we need to split these two classes
public abstract class SnapShotBase : MonoBehaviour
{
    public abstract event Action<SnapShot> IsDestroy;
    public abstract float Damage { get; protected set; }
    public abstract float Speed { get; protected set; }
    public abstract float TimeLife { get; protected set; }
    public abstract Vector2 Direction { get; protected set; }
    public abstract Rigidbody2D SnapShotModel { get; protected set; }
    public abstract SpaceShip Owner { get; protected set; }

    public virtual void Initializing(float Damage, float TimeLife, Vector2 Direction, SpaceShip Owner) => Debug.Log($"SnapShot {this.GetType()} Initializing()");
    public virtual void OnUpdate() => Debug.Log($"SnapShot {this.GetType()} OnUpdate()");
}

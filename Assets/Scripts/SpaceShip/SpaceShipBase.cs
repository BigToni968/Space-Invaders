using UnityEngine;
using System;

public abstract class SpaceShipBase : MonoBehaviour
{
    public abstract event Action<SpaceShip> IsDead;
    public abstract float Life { get; protected set; }
    public abstract float Speed { get; protected set; }
    //Prefab
    protected abstract SpaceShipBody SpaceShipBody { get; set; }
    protected abstract Rigidbody2D SpaceShipModel { get; set; }
    public virtual string GetInfo() => $"Space Ship {this.GetType()}, Life = {Life}, Speed = {Speed}";
    public virtual void OnFire() => Debug.Log($"Space Ship {this.GetType()} OnFire()");
    public virtual void OnUpdate() => Debug.Log($"Space Ship {this.GetType()} OnUpdate()");
    public virtual void OnHit(float Damage) => Debug.Log($"Space Ship {this.GetType()} OnHit({Damage})");
}

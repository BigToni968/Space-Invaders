using UnityEngine;
using System;

public class SpaceShip : SpaceShipBase
{
    public override event Action<SpaceShip> IsDead = delegate { };
    public override float Life { get; protected set; }
    public override float Speed { get; protected set; }
    protected override Rigidbody2D SpaceShipModel { get; set; }
    protected override SpaceShipBody SpaceShipBody { get; set; }

    public virtual void Initializing(SpaceShipData SpaceShipData)
    {
        Life = SpaceShipData.Life;
        Speed = SpaceShipData.Speed;
        SpaceShipModel = GetComponentInChildren<Rigidbody2D>();
        SpaceShipBody = SpaceShipModel.GetComponent<SpaceShipBody>();
    }

    protected virtual void OnMove(Vector2 direction)
    {
        Vector2 veclocity = Vector2.zero;
        direction *= Speed;
        veclocity.Set(direction.x, direction.y);
        SpaceShipModel.velocity = veclocity;
    }

    public override void OnHit(float Damage)
    {
        base.OnHit(Damage);
        Life -= Damage;
        if (Life < 0f) Life = 0f;
        if (Life == 0f) IsDead?.Invoke(this);
    }

    public virtual void SubscribeShipEvents() => Debug.Log($"Subscribe to {this.GetType()} Events");
    public virtual void UnSubscribeShipEvents() => Debug.Log($"UnSubscribe to {this.GetType()} Events");
}

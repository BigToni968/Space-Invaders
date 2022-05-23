using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public abstract GunData GunData { get; protected set; }
    public abstract SnapShot SnapShot { get; protected set; }
    public abstract float Damage { get; protected set; }
    public abstract float TimeLife { get; protected set; }

    public virtual void Initializing()
    {
        SnapShot = GunData.SnapShot.GetComponent<SnapShot>();
        Damage = GunData.Damage;
        TimeLife = GunData.TimeLife;
    }

    public virtual void OnAttack(SpaceShip Owner, Vector2 Direction) => Debug.Log($"Space Ship {Owner.GetType()} execute  Gun {this.GetType()} OnAttack");
    public virtual void OnUpdate() => Debug.Log($"Gun {this.GetType()} OnUpdate");
}

using UnityEngine;

// Are you sure we need to split GunBase and Gun? They seem to be nearly identical
public abstract class GunBase : MonoBehaviour
{
    public abstract GunData GunData { get; protected set; }
    public abstract SnapShot SnapShot { get; protected set; }
    
    // Tip: in this case you can use 
    // public float Damage => GunData.Damage;
    public abstract float Damage { get; protected set; }
    // Same here
    // Also, it would be more grammatically correct to say LifeTime, not TimeLife
    public abstract float TimeLife { get; protected set; }

    public virtual void Initializing()
    {
        // There is no need to use GetComponent on GunData.SnapShot because it is already of the type SnapShot
        // SnapShot = GunData.SnapShot would work just fine
        SnapShot = GunData.SnapShot.GetComponent<SnapShot>();
        Damage = GunData.Damage;
        TimeLife = GunData.TimeLife;
    }

    public virtual void OnAttack(SpaceShip Owner, Vector2 Direction) => Debug.Log($"Space Ship {Owner.GetType()} execute  Gun {this.GetType()} OnAttack");
    public virtual void OnUpdate() => Debug.Log($"Gun {this.GetType()} OnUpdate");
}

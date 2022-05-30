using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunData _gunData = null;
    public override GunData GunData { get => _gunData; protected set => _gunData = value; }
    public override SnapShot SnapShot { get; protected set; }
    
    // You never assign new values to Damage so there is no need to override it
    public override float Damage { get; protected set; }
    // Same here
    public override float TimeLife { get; protected set; }

    protected List<SnapShot> _launchedProjectiles = new List<SnapShot>(0);

    protected void Awake() => Initializing();

    public override void OnAttack(SpaceShip Owner, Vector2 Direction)
    {
        SnapShot snapShot = Instantiate(SnapShot);
        snapShot.Initializing(Damage, TimeLife, Direction, Owner);
        snapShot.IsDestroy += DestroyProjectiles;
        _launchedProjectiles.Add(snapShot);
        
        // This approach to creating projectiles does work, but it can put quite a strain on garbage collection.
        // Please read on "Object Pool" pattern.
    }

    public override void OnUpdate()
    {
        for (byte i = 0; i < _launchedProjectiles.Count; i++)
            _launchedProjectiles[i].OnUpdate();
    }

    // The method is called DestroyProjectiles, but you only destroy 1 projectile
    // So it would be better to call it DestroyProjectile
    protected virtual void DestroyProjectiles(SnapShot SnapShot)
    {
        SnapShot.IsDestroy -= DestroyProjectiles;
        _launchedProjectiles.Remove(SnapShot);
        Destroy(SnapShot.gameObject);
    }
}

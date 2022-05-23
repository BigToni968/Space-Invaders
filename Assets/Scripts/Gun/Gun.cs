using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunData _gunData = null;
    public override GunData GunData { get => _gunData; protected set => _gunData = value; }
    public override SnapShot SnapShot { get; protected set; }
    public override float Damage { get; protected set; }
    public override float TimeLife { get; protected set; }

    protected List<SnapShot> _launchedProjectiles = new List<SnapShot>(0);

    protected void Awake() => Initializing();

    public override void OnAttack(SpaceShip Owner, Vector2 Direction)
    {
        SnapShot snapShot = Instantiate(SnapShot);
        snapShot.Initializing(Damage, TimeLife, Direction, Owner);
        snapShot.IsDestroy += DestroyProjectiles;
        _launchedProjectiles.Add(snapShot);
    }

    public override void OnUpdate()
    {
        for (byte i = 0; i < _launchedProjectiles.Count; i++)
            _launchedProjectiles[i].OnUpdate();
    }

    protected virtual void DestroyProjectiles(SnapShot SnapShot)
    {
        SnapShot.IsDestroy -= DestroyProjectiles;
        _launchedProjectiles.Remove(SnapShot);
        Destroy(SnapShot.gameObject);
    }
}

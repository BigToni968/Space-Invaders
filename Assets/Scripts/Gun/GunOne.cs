using UnityEngine;

public class GunOne : Gun
{
    public override void OnAttack(SpaceShip Owner, Vector2 Direction)
    {
        SnapShot snapShot = Instantiate(SnapShot);
        snapShot.transform.position = transform.GetChild(transform.childCount - 1).position;
        snapShot.IsDestroy += DestroyProjectiles;
        snapShot.Initializing(Damage, TimeLife, Direction, Owner);
        _launchedProjectiles.Add(snapShot);
    }
}
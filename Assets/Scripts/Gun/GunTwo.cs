using UnityEngine;

public class GunTwo : Gun
{
    [SerializeField] private Transform[] _snapShotStartPoss = null;

    // Check notes to GunOne
    public override void OnAttack(SpaceShip Owner, Vector2 Direction)
    {
        if (_snapShotStartPoss != null)
        {
            for (int i = 0; i < _snapShotStartPoss.Length; i++)
            {
                SnapShot snapShot = Instantiate(SnapShot);
                snapShot.transform.position = _snapShotStartPoss[i].position;
                snapShot.IsDestroy += DestroyProjectiles;
                snapShot.Initializing(Damage, TimeLife, Direction, Owner);
                _launchedProjectiles.Add(snapShot);
            }
        }
    }
}

using UnityEngine;

public class GunOne : Gun
{
    public override void OnAttack(SpaceShip Owner, Vector2 Direction)
    {
        // This bit of code seems copy pasted between GunOne and GunTwo. This is a bad practice.
        // This code should have been moved to a parent class and used from there.
        // The reason why so is because if you would want to make a change in this code, you would have to make it twice, which is obviously not good.
        // Please read on DRY (Don't Repeat Yourself) principle
        
        SnapShot snapShot = Instantiate(SnapShot);
        
        // transform.GetChild(transform.childCount - 1) is not a good idea here, because it makes debugging much more difficult and confusing
        // Instead we can add a field to the class like this:
        // [SerializeField] private Transform snapShotSpawnPoint;
        // and then do
        // snapShot.transform.position = snapShotSpawnPoint.position;
        snapShot.transform.position = transform.GetChild(transform.childCount - 1).position;
        snapShot.IsDestroy += DestroyProjectiles;
        snapShot.Initializing(Damage, TimeLife, Direction, Owner);
        _launchedProjectiles.Add(snapShot);
    }
}
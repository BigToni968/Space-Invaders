using System;

public class PlayerController : Controller
{
    public event Action DestroyedConttroller = delegate { };
    protected override void ShipDead(SpaceShip SpaceShip)
    {
        base.ShipDead(SpaceShip);
        DestroyedConttroller?.Invoke();
        Destroy(gameObject);
    }
}

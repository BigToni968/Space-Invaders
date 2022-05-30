using System;

public class PlayerController : Controller
{
    // Maybe a better name would be "PlayerShipDestroyed" or something
    // Also, it's not necessary to assign default values to events, so you could have left it at 
    // public event Action DestroyedConttroller;
    public event Action DestroyedConttroller = delegate { };
    protected override void ShipDead(SpaceShip SpaceShip)
    {
        base.ShipDead(SpaceShip);
        DestroyedConttroller?.Invoke();
        Destroy(gameObject);
    }
}

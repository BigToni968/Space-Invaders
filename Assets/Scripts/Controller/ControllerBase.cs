using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public abstract SpaceShipData SpaceShipData { get; protected set; }
    public abstract SpaceShip SpaceShip { get; protected set; }

    //Init prefab Space ship
    public virtual void Initializing() => Debug.Log($"Controller {this.GetType()} Initializing");
}

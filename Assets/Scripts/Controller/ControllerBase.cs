using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    // Since controller is the only class that is inhereting ControllerBase, I think it's not necessary to split them
    
    public abstract SpaceShipData SpaceShipData { get; protected set; }
    public abstract SpaceShip SpaceShip { get; protected set; }

    //Init prefab Space ship
    public virtual void Initializing() => Debug.Log($"Controller {this.GetType()} Initializing");
}

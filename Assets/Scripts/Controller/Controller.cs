using UnityEngine;

public class Controller : ControllerBase
{
    // It was a good idea to inherit PlayerController and UnarmedController from Controller,
    // However since ArmedController and MotherController are not different from UnarmedController,
    // I think these two classes are unnecessary
    // Maybe we should have called UnarmedController "AiController" instead and use it for all 3 cases (Unarmed, Armed, Mother)
    
    // Oh yes and maybe as the base for the name we should have used "[Prefix]SpaceShipController" instead of "[Prefix]Controller"
    // for more clarity
    
    [SerializeField] private SpaceShipData _spaceShipData = null; // null is the default value for reference type objects, so no need to specify
    
    // _spaceShipData could be removed and the property shortened to
    // public override SpaceShipData SpaceShipData { get; protected set; }
    public override SpaceShipData SpaceShipData { get => _spaceShipData; protected set => _spaceShipData = value; }
    public override SpaceShip SpaceShip { get; protected set; }

    // Verb infinitive would have been better suited here instead of gerund (so Initialize instead of Initializing)
    public override void Initializing()
    {
        base.Initializing();
        SpaceShip = Instantiate(SpaceShipData.SpaceShipModel, transform).GetComponent<SpaceShip>();
        SpaceShip.Initializing(SpaceShipData);
    }

    // Seems like a poor name choice. Did you mean "DestroyShip" or "KillShip"?
    // It's generally a good practice to use Verbs for method names, instead of Adjectives
    protected virtual void ShipDead(SpaceShip SpaceShip)
    {
        SpaceShip.IsDead -= ShipDead;
        Destroy(SpaceShip.gameObject);
    }

    protected void Awake() => Initializing();

    protected void OnEnable()
    {
        // Exposing .SubscribeShipEvents() seems like a violation of encapsulation
        // We don't need to let others know the inner workings of a SpaceShip.
        // This should probably be moved to somewhere within SpaceShip itself
        SpaceShip.SubscribeShipEvents();
        SpaceShip.IsDead += ShipDead;
    }
    protected void OnDisable()
    {
        SpaceShip.UnSubscribeShipEvents();
        SpaceShip.IsDead -= ShipDead;
    }
    
    // OnDisable will be called anyway when the object is destroyed so there is no need to call it explicitly
    protected void OnDestroy() => OnDisable();

}

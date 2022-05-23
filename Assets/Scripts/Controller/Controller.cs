using UnityEngine;

public class Controller : ControllerBase
{
    [SerializeField] private SpaceShipData _spaceShipData = null;
    public override SpaceShipData SpaceShipData { get => _spaceShipData; protected set => _spaceShipData = value; }
    public override SpaceShip SpaceShip { get; protected set; }

    public override void Initializing()
    {
        base.Initializing();
        SpaceShip = Instantiate(SpaceShipData.SpaceShipModel, transform).GetComponent<SpaceShip>();
        SpaceShip.Initializing(SpaceShipData);
    }

    protected virtual void ShipDead(SpaceShip SpaceShip)
    {
        SpaceShip.IsDead -= ShipDead;
        Destroy(SpaceShip.gameObject);
    }

    protected void Awake() => Initializing();

    protected void OnEnable()
    {
        SpaceShip.SubscribeShipEvents();
        SpaceShip.IsDead += ShipDead;
    }
    protected void OnDisable()
    {
        SpaceShip.UnSubscribeShipEvents();
        SpaceShip.IsDead -= ShipDead;
    }
    protected void OnDestroy() => OnDisable();

}

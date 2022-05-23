using UnityEngine;

public class ArmedShip : UnarmedShip
{
    [SerializeField] private Gun _gun = null;

    private Timer _timer = new Timer();
    protected bool _insidePlayArea = false;

    private SpaceShipArmedData _spaceShipArmedData = null;

    private void Start()
    {
        if (_gun != null)
            _gun = Instantiate(_gun, GetComponentsInChildren<Transform>()[1]);
    }

    public override void Initializing(SpaceShipData SpaceShipData)
    {
        base.Initializing(SpaceShipData);
        _spaceShipArmedData = SpaceShipData as SpaceShipArmedData;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_timer.Counting(_spaceShipArmedData.DelayAttack)) OnAttack();
        if (_gun != null) _gun.OnUpdate();
    }

    protected override void EnretCollider(Collider2D collider)
    {
        base.EnretCollider(collider);
        if (collider.tag.Equals("PlayArea"))
            _insidePlayArea = true;
    }

    protected override void ExitCollider(Collider2D collider)
    {
        base.ExitCollider(collider);
        if (collider.tag.Equals("PlayArea"))
            _insidePlayArea = false;
    }

    public virtual void OnAttack()
    {
        if (_gun != null)
            if (_insidePlayArea)
                _gun.OnAttack(this, Vector2.down);
    }

}

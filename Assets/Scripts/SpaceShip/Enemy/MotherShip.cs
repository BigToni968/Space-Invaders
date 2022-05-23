using System.Collections;
using UnityEngine;

public class MotherShip : ArmedShip
{
    private SpaceShipMotherData _spaceShipMotherData = null;
    private (float Left, float Right) _chamberboundary = default;
    private Camera _camera = null;
    private Timer _timer = new Timer();
    private bool _maneuvrActivation = false;
    private bool _maneuvrSucceeded = false;
    private bool _notConditionsManeuvr = false;

    public override void Initializing(SpaceShipData SpaceShipData)
    {
        base.Initializing(SpaceShipData);
        _spaceShipMotherData = SpaceShipData as SpaceShipMotherData;
        _camera = FindObjectOfType<Camera>();
        _chamberboundary.Left = _camera.transform.position.x - _camera.Rectangle().Widht;
        _chamberboundary.Right = _camera.transform.position.x + _camera.Rectangle().Widht;
        _timeDelay = _spaceShipMotherData.DelayManeuver;
    }

    protected override void EnretCollider(Collider2D collider)
    {
        if (!_maneuvrActivation && !_insidePlayArea)
        {
            base.EnretCollider(collider);
            return;
        }

        if (collider.tag.Equals("SnapShot"))
        {
            SnapShot snapShot = collider.GetComponent<SnapShot>();
            if (snapShot.Owner != null)
                if (!snapShot.Owner.tag.Equals(tag)) OnHit(snapShot.Damage);
        }


        if (collider.tag.Equals("PlayArea"))
            _insidePlayArea = true;

        if (collider.tag.Equals("Wall") && !_insidePlayArea)
            _maneuvrActivation = false;

        if (collider.tag.Equals("Wall") && _insidePlayArea)
            if (_notConditionsManeuvr)
                ChangeDirection();

        if (collider.tag.Equals("Wall") && _insidePlayArea && _maneuvrSucceeded)
            ChangeDirection();
    }

    protected override void ExitCollider(Collider2D collider)
    {
        if (!_maneuvrActivation && !_insidePlayArea)
        {
            base.ExitCollider(collider);
            return;
        }

        if (collider.tag.Equals("PlayArea"))
            _insidePlayArea = false;

        if (collider.tag.Equals("Wall") && !_insidePlayArea)
            OnManeuvr();
    }

    protected virtual void OnManeuvr()
    {
        Vector2 curentPoss = SpaceShipModel.transform.position;
        if (_direction == Vector2.right) curentPoss.x = _chamberboundary.Left - SpaceShipModel.transform.localScale.x * 2;
        if (_direction == Vector2.left) curentPoss.x = _chamberboundary.Right + SpaceShipModel.transform.localScale.x * 2;
        curentPoss.y = _camera.transform.position.y + _camera.Rectangle().Height - SpaceShipModel.transform.localScale.y;
        SpaceShipModel.transform.position = curentPoss;
        _maneuvrSucceeded = true;
    }

    private void ChangeDirection()
    {
        _maneuvrSucceeded = false;
        _direction *= -1;
    }

    public override IEnumerator Rebuild()
    {
        if (!_insidePlayArea & !_maneuvrActivation)
            yield return base.Rebuild();

        yield return null;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!_maneuvrActivation && _insidePlayArea)
            if (_timer.Counting(_timeDelay))
                _maneuvrActivation = Random.Range(0, 100) <= 40;

        _notConditionsManeuvr = !_maneuvrActivation && !_maneuvrSucceeded;
    }
}




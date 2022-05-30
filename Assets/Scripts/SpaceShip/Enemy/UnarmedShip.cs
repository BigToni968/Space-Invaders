using System.Collections;
using UnityEngine;
using System;

public class UnarmedShip : SpaceShip
{
    // you never override this event so no need to make it virtual
    public virtual event Action IsWall = delegate { };

    public bool IsRebuild { get; protected set; }

    protected Vector2 _direction = Vector2.right;
    protected float _distance, _timeDelay, _acceleration;

    private SpaceShipUnarmedData _spaceShipUnarmedData = null;
    protected bool SelfDistruction { get; set; } = false;
    protected GameManager _gameManager = null;

    private void Start() => IsRebuild = false;

    // Initializing -> Initialize
    public override void Initializing(SpaceShipData SpaceShipData)
    {
        base.Initializing(SpaceShipData);
        _spaceShipUnarmedData = SpaceShipData as SpaceShipUnarmedData;
        _gameManager = FindObjectOfType<GameManager>(); // Please never use Find

        _direction = GetDirection.GetVector(_spaceShipUnarmedData.DefaultDirection);
        _distance = _spaceShipUnarmedData.DistanceForward;
        _timeDelay = _spaceShipUnarmedData.DelayStep;
        _acceleration = _spaceShipUnarmedData.Acceleration;
    }

    // These two methods should not be public, because that would violate encapsulation
    public override void SubscribeShipEvents()
    {
        base.SubscribeShipEvents();
        SpaceShipBody.EnterCollider += EnretCollider;
        SpaceShipBody.ExitCollider += ExitCollider;
        IsDead += OnDead;
    }

    public override void UnSubscribeShipEvents()
    {
        base.UnSubscribeShipEvents();
        SpaceShipBody.EnterCollider -= EnretCollider;
        SpaceShipBody.ExitCollider -= ExitCollider;
        IsDead -= OnDead;
    }

    public override void OnUpdate() => OnMove(_direction);

    // Using infinitive Verb form seems like not a good idea here
    // Consider using names like OnColliderEntered or ProcessCollision
    protected virtual void EnretCollider(Collider2D collider)
    {
        if (collider.tag.Equals("SnapShot"))
        {
            SnapShot snapShot = collider.GetComponent<SnapShot>();
            if (snapShot.Owner != null)
                if (!snapShot.Owner.tag.Equals(tag)) OnHit(snapShot.Damage);
        }

        if (collider.tag.Equals("Wall")) IsWall?.Invoke();
    }

    protected virtual void ExitCollider(Collider2D collider)
    {
        if (collider.tag.Equals("PlayArea"))
        {
            SelfDistruction = true;
            OnHit(Life);
        }
    }

    protected virtual void OnDead(SpaceShip SpaceShip)
    {
        if (!SelfDistruction)
        {
            _gameManager.GUI.Score.Add(ScoreValue.Score, _spaceShipUnarmedData.DeadPoints);
            _gameManager.GUI.AppearingText.Spawn(SpaceShipBody.transform.position, _spaceShipUnarmedData.DeadPoints);
        }
    }

    // I added some empty lines for better readability
    public virtual IEnumerator Rebuild()
    {
        IsRebuild = true;
        Vector2 Direction = Vector2.zero;
        
        if (SpaceShipModel != null) 
            SpaceShipModel.velocity = Direction;
        
        Vector2 newPossition = SpaceShipModel.transform.position;
        Direction = GetDirection.GetVector(_spaceShipUnarmedData.StepDirection) * _distance;
        newPossition += Direction;
        SpaceShipModel.transform.position = newPossition;

        yield return new WaitForSeconds(_timeDelay);

        Speed += _acceleration;
        _direction *= -1;
        IsRebuild = false;
    }
}

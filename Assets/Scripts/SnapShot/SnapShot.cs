using UnityEngine;
using System;

public class SnapShot : SnapShotBase
{
    [SerializeField] private float _speed = 0f;

    public override event Action<SnapShot> IsDestroy = delegate { };
    public override float Damage { get; protected set; }
    public override float Speed { get => _speed; protected set => _speed = value; }
    public override float TimeLife { get; protected set; }
    public override Rigidbody2D SnapShotModel { get; protected set; }
    public override Vector2 Direction { get; protected set; }
    public override SpaceShip Owner { get; protected set; }

    private float _timePassed = 0;

    private void Start() => SnapShotModel = GetComponent<Rigidbody2D>();

    public override void Initializing(float Damage, float TimeLife, Vector2 Direction, SpaceShip Owner)
    {
        this.Damage = Damage;
        this.TimeLife = TimeLife;
        this.Direction = Direction;
        this.Owner = Owner;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy") || collision.tag.Equals("Player"))
            if (Owner != null)
                if (!collision.tag.Equals(Owner.tag)) IsDestroy?.Invoke(this);
    }

    private void Move(Vector2 Direction)
    {
        Vector2 velocity = Vector2.zero;
        velocity.Set(Direction.x, Direction.y * Speed);
        if (SnapShotModel != null) SnapShotModel.velocity = velocity;
    }

    public override void OnUpdate()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed > TimeLife) IsDestroy?.Invoke(this);
        Move(Direction);
    }
}

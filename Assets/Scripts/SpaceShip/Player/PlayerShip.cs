using UnityEngine;

public class PlayerShip : SpaceShip
{
    [SerializeField] private Gun _gun = null;
    private GameManager _gameManager = null;
    private Camera _camera = null;

    private void Start()
    {
        // Please never ever use Find
        _camera = FindObjectOfType<Camera>();
        _gameManager = FindObjectOfType<GameManager>();
        
        // Please never use GetComponent when it's possible to use SerializeField
        _gun = Instantiate(_gun, GetComponentsInChildren<Transform>()[1]);
        _gameManager.GUI.Score.ResetValue(ScoreValue.Life, Life);
    }
    
    // Did you mean "OnCollision"?
    private void OnCollider(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy")) OnHit(Life);

        if (collision.tag.Equals("SnapShot"))
        {
            // This is a good example where using GetComponent is ok :)
            // In most other cases [SerializeField] is your friend
            SnapShot snapShot = collision.GetComponent<SnapShot>();

            if (snapShot.Owner != null)
                if (!snapShot.Owner.tag.Equals(tag)) OnHit(snapShot.Damage);
        }
    }

    // These two methods should not be public, because that would violate encapsulation
    public override void SubscribeShipEvents()
    {
        base.SubscribeShipEvents();
        SpaceShipBody.EnterCollider += OnCollider;
    }

    public override void UnSubscribeShipEvents()
    {
        base.UnSubscribeShipEvents();
        SpaceShipBody.EnterCollider -= OnCollider;
    }

    // The name of this method is very confusing, I'm not even sure what did you mean.
    // CorrectShipPosition?
    protected virtual void CorrectionFlaySpaceShip()
    {
        Vector2 currentPosition = SpaceShipModel.transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x,
            _camera.transform.position.x - _camera.Rectangle().Widht + SpaceShipModel.transform.localScale.x / 2,
            _camera.transform.position.x + _camera.Rectangle().Widht - SpaceShipModel.transform.localScale.x / 2);

        currentPosition.y = Mathf.Clamp(currentPosition.y,
            _camera.transform.position.y - _camera.Rectangle().Height + SpaceShipModel.transform.localScale.y / 2,
            _camera.transform.position.y + _camera.Rectangle().Height - SpaceShipModel.transform.localScale.y / 2);

        SpaceShipBody.transform.position = currentPosition;
    }

    public override void OnUpdate()
    {
        // Not sure if this belongs here. Maybe we should have moved this to some kind of InputManager or PlayerController class.
        // Also, this method feels a bit cramped, maybe we should have left more empty lines.
        OnMove(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (Input.GetKeyDown(KeyCode.Space)) OnFire();
        if (_gun != null) _gun.OnUpdate(); // you can use _gun?.OnUpdate() instead
        CorrectionFlaySpaceShip();
    } // Please leave 1 empty line between methods
    public override void OnHit(float Damage)
    {
        base.OnHit(Damage);
        _gameManager.GUI.Score.Add(ScoreValue.Life, -Damage);
    }
    public override void OnFire()
    {
        base.OnFire();
        if (_gun != null) _gun.OnAttack(this, Vector2.up);
    }
}

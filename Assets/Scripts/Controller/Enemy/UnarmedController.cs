using System;

public class UnarmedController : Controller
{
    public event Action<UnarmedController> IsRebuild = delegate { };

    private UnarmedShip _unarmed = null;

    private new void Awake()
    {
        base.Awake();
        _unarmed = SpaceShip as UnarmedShip;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        _unarmed.IsWall += IndependentRebuildingShip;
    }
    private new void OnDisable()
    {
        base.OnDisable();
        _unarmed.IsWall -= IndependentRebuildingShip;
    }

    public virtual void IndependentRebuildingShip()
    {
        IsRebuild?.Invoke(this);
        ForcedRestructingShip();
    }

    public virtual void ForcedRestructingShip()
    {
        if (_unarmed != null) StartCoroutine(_unarmed.Rebuild());
    }

    public virtual void OnUpdate()
    {
        if (_unarmed != null)
            if (!_unarmed.IsRebuild) SpaceShip.OnUpdate();
    }
}

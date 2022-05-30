using System;

public class UnarmedController : Controller
{
    public event Action<UnarmedController> IsRebuild = delegate { };

    // It's generally a good idea to use Nouns as field names. Using Adjectives should only be done in some specific cases like boolean fields
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

    // It's better to use Verbs as method names, for example "RebuildShip()"
    // Also, there is no need to make a method virtual if you're not going to override it
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

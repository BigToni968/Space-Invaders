public class WaveUnarmed : Wave
{
    protected void Awake()
    {
        Initializing();
        Instantiate();
    }

    protected virtual void SubscriptionDeadShip()
    {
        foreach (UnarmedController controller in Controllers)
            controller.SpaceShip.IsDead += CalculatingShipDestroy;
    }

    protected virtual void UnSubscriptionDeadShip()
    {
        foreach (UnarmedController controller in Controllers)
            controller.SpaceShip.IsDead -= CalculatingShipDestroy;
    }

    protected virtual void SubscriptionRebuildShip()
    {
        foreach (UnarmedController controller in Controllers)
            controller.IsRebuild += ForcedRestructedSpaceShips;
    }

    protected virtual void UnSubscriptionRebuildShip()
    {
        foreach (UnarmedController controller in Controllers)
            controller.IsRebuild -= ForcedRestructedSpaceShips;
    }

    protected virtual void ForcedRestructedSpaceShips(UnarmedController UnarmedController)
    {
        foreach (UnarmedController unarmedController in Controllers)
            if (!ReferenceEquals(unarmedController, UnarmedController)) unarmedController.ForcedRestructingShip();
    }

    protected void OnEnable()
    {
        SubscriptionDeadShip();
        SubscriptionRebuildShip();
    }
    protected void OnDisable()
    {
        UnSubscriptionDeadShip();
        UnSubscriptionRebuildShip();
    }
    protected void OnDestroy() => OnDisable();

    public override void OnUpdate()
    {
        foreach (UnarmedController controller in Controllers)
            controller.OnUpdate();
    }

}

public class JumpingPad : LevelObject
{
    private bool isEnabled;

    protected override void InitializeBehaviors()
    {
        base.InitializeBehaviors();

        var actComponent = GetBehavior<ActivableComponent>();
        var stateComponent = GetBehavior<StateComponent>();
        var lightComponent = GetBehavior<LightEmmiterComponent>();

        actComponent.Activate += MoveObject;
    }

    private void MoveObject(ActivationData data)
    {
        if (!isEnabled) return;

        var objectIntile = data.tile.GetObject(TileLayer.OBJECT);
        if (!objectIntile) return;

        var movComponent = objectIntile.GetBehavior<MovableComponent>();
    }
}

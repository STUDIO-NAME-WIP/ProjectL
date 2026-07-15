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
        stateComponent.OnTurnOff += () => lightComponent.enabled = false;
        stateComponent.OnTurnOn += () => lightComponent.enabled = true;
    }

    private void MoveObject(ActivationData data)
    {
        if (!isEnabled) return;

        var objectIntile = data.tile.GetObject(TileLayer.OBJECT);
        if (!objectIntile) return;
        MovementData movData = new MovementData
        {
            obj = objectIntile,
            originTile = data.tile
        };

        var movComponent = objectIntile.GetBehavior<MovableComponent>();

        if (movComponent.CanMove(movData)) movComponent.Move(movData);
    }
}

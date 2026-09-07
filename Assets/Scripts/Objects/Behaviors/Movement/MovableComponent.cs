using UnityEngine;

public class MovableComponent : IObjectBehavior
{
    private IMovableBehavior movableBehavior;

    public MovableType MovableType { get; private set; }
    public MovementType MovementType { get; private set; }

    public bool IsPushable { get; set; }

    public bool IsMovable =>
        movableBehavior != null &&
        movableBehavior.IsMovable;

    public bool IsMoving =>
        movableBehavior != null &&
        movableBehavior.IsMoving;

    public void Configure(LevelObjectParameters data)
    {
        MovableType = data.movableType;
        MovementType = data.movementType;

        movableBehavior = data.movableType switch
        {
            MovableType.IMMOVABLE => new ImmovableBehavior(),
            MovableType.TILE_RESTRICTED => new TileRestrictedMovementBehavior(data.moveSpeed),
            MovableType.CONTINUOUS => new ContinuosMovementBehavior(data.moveSpeed),
            _ => new ImmovableBehavior()
        };
    }

    public bool CanMove(MovementData data)
    {
        if (!IsMovable) return false;
        return movableBehavior.CanMove(data);
    }

    public bool TryMove(LevelObject obj, Direction direction)
    {
        return TryMove(obj, (Vector2)direction.ToVector2Int());
    }

    public bool TryMove(LevelObject obj, Vector2 direction)
    {
        if (!IsMovable || obj == null || direction.sqrMagnitude < 0.0001f)
            return false;

        return movableBehavior.TryMove(obj, direction);
    }

    public void Move(MovementData data)
    {
        if (!IsMovable) return;
        movableBehavior.Move(data);
    }
}

public enum MovableType
{
    IMMOVABLE,
    TILE_RESTRICTED,
    CONTINUOUS
}

public enum MovementType
{
    SWIMMING,
    WALKING,
    HOVERING
}

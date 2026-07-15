using UnityEngine;

public class MovableComponent : MonoBehaviour, IObjectBehavior
{
    private IMovableBehavior movableBehavior;
    private Tile currentTile;
    private TileLayer layer;

    public void Configure(LevelObjectParameters data)
    {
        switch (data.movableType)
        {
            case MovableType.IMMOVABLE:
                movableBehavior = new ImmovableBehavior();
                break;
            case MovableType.TILE_RESTRICTED:
                movableBehavior = new TileRestrictedMovementBehavior(data.moveSpeed);
                break;
            case MovableType.CONTINUOUS:
                movableBehavior = new ContinuosMovementBehavior(data.moveSpeed);
                break;
        }
    }

    public bool CanMove(MovementData data) => movableBehavior.CanMove(data);

    public void Move(MovementData data)
    {
        currentTile?.RemoveObject(layer);
        data.targetTile.TryPlaceObject(layer, data.obj);
        movableBehavior.Move(data);
        currentTile = data.targetTile;
    }

    public void SetCurrentTile(Tile tile, TileLayer layer)
    {
        currentTile = tile;
        this.layer = layer;
    }

    public Tile GetCurrentTile() => currentTile;
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
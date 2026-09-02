using UnityEngine;

public struct MovementData
{
    public LevelObject Object { get; }
    public Tile OriginTile { get; }
    public Tile TargetTile { get; }
    public Vector3 TargetWorldPosition { get; }
    public bool ChangesTile => OriginTile != TargetTile;

    public MovementData(
        LevelObject obj,
        Tile originTile,
        Tile targetTile,
        Vector3 targetWorldPosition)
    {
        Object = obj;
        OriginTile = originTile;
        TargetTile = targetTile;
        TargetWorldPosition = targetWorldPosition;
    }
}

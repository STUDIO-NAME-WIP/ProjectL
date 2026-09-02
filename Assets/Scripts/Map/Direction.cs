using UnityEngine;

public enum Direction
{
    NORTH, EAST, SOUTH, WEST
}

public static class DirectionExtensions
{
    public static Direction ToDirection(this Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            return direction.x > 0f ? Direction.EAST : Direction.WEST;

        return direction.y > 0f ? Direction.NORTH : Direction.SOUTH;
    }

    public static Vector2Int ToVector2Int(this Direction dir)
    {
        return dir switch
        {
            Direction.NORTH => new Vector2Int(0, 1),
            Direction.EAST => new Vector2Int(1, 0),
            Direction.SOUTH => new Vector2Int(0, -1),
            Direction.WEST => new Vector2Int(-1, 0),
            _ => Vector2Int.zero,
        };
    }

    public static Quaternion ToRotation(this Direction dir)
    {
        return dir switch
        {
            Direction.NORTH => Quaternion.Euler(0, 0, 0),
            Direction.EAST => Quaternion.Euler(0, 90, 0),
            Direction.SOUTH => Quaternion.Euler(0, 180, 0),
            Direction.WEST => Quaternion.Euler(0, 270, 0),
            _ => Quaternion.identity,
        };
    }

    public static Direction RotateClockwise(this Direction dir)
    {
        return (Direction)(((int)dir + 1) % 4);
    }

    public static Direction RotateCounterClockwise(this Direction dir)
    {
        return (Direction)(((int)dir + 3) % 4);
    }
}

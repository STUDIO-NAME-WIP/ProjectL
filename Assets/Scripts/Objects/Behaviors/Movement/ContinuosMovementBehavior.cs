using UnityEngine;

public class ContinuosMovementBehavior : IMovableBehavior
{
    private readonly float speed;
    public ContinuosMovementBehavior(float speed = 5f)
    {
        this.speed = speed;
    }

    public bool IsMovable => true;
    public bool IsMoving => false;

    public bool TryMove(LevelObject obj, Vector2 direction)
    {
        if (obj.Map == null)
            return false;

        Vector2 normalizedDirection = direction.normalized;
        Vector3 worldDirection = new Vector3(normalizedDirection.x, 0f, normalizedDirection.y);
        Vector3 targetPosition = obj.transform.position +
                                 worldDirection * speed * Time.deltaTime;

        return obj.Map.TryMoveContinuously(obj, targetPosition);
    }

    public bool CanMove(MovementData data)
    {
        return data.OriginTile != null && data.TargetTile != null;
    }

    public void Move(MovementData data)
    {
        data.Object.transform.position = data.TargetWorldPosition;
    }
}

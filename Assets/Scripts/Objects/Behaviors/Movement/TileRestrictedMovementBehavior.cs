using UnityEngine;
using System.Collections;

public class TileRestrictedMovementBehavior : IMovableBehavior
{
    private readonly float speed;
    private bool isMoving;

    public TileRestrictedMovementBehavior(float speed = 5f)
    {
        this.speed = speed;
    }

    public bool IsMovable => true;
    public bool IsMoving => isMoving;

    public bool TryMove(LevelObject obj, Vector2 direction)
    {
        return obj.Map != null && obj.Map.TryMoveToAdjacentTile(obj, direction.ToDirection());
    }

    public bool CanMove(MovementData data)
    {
        return data.OriginTile != null &&
               data.TargetTile != null &&
               data.ChangesTile;
    }

    public void Move(MovementData data)
    {
        data.Object.StartCoroutine(
            MoveRoutine(
                data.Object.transform,
                data.TargetWorldPosition));
    }

    private IEnumerator MoveRoutine(
        Transform obj,
        Vector3 targetPosition)
    {
        isMoving = true;
        while (Vector3.Distance(
            obj.position,
            targetPosition) > 0.01f)
        {
            obj.position = Vector3.MoveTowards(
                obj.position,
                targetPosition,
                speed * Time.deltaTime);

            yield return null;
        }

        obj.position = targetPosition;
        isMoving = false;
    }
}

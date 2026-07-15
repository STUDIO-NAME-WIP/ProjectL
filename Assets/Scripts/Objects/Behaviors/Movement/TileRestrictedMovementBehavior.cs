using UnityEngine;

public class TileRestrictedMovementBehavior : IMovableBehavior
{
    private readonly float speed;

    public TileRestrictedMovementBehavior(float speed = 5f)
    {
        this.speed = speed;
    }

    public bool CanMove(MovementData data)
    {
        return data.targetTile != null && data.targetTile.IsEmpty(data.layer);
    }

    public void Move(MovementData data)
    {
        data.obj.StartCoroutine(MoveRoutine(data.obj.transform, data.targetTile.WorldPosition));
    }

    private System.Collections.IEnumerator MoveRoutine(Transform obj, Vector3 targetPos)
    {
        while (Vector3.Distance(obj.position, targetPos) > 0.01f)
        {
            obj.position = Vector3.MoveTowards(obj.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        obj.position = targetPos;
    }
}

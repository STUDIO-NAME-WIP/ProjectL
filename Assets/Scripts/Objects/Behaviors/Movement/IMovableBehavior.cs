using UnityEngine;

public interface IMovableBehavior
{
    bool IsMovable { get; }
    bool IsMoving { get; }

    bool TryMove(LevelObject obj, Vector2 direction);
    bool CanMove(MovementData data);
    void Move(MovementData data);
}

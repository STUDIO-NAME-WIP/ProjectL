using UnityEngine;

public class ImmovableBehavior : IMovableBehavior
{
    public bool IsMovable => false;
    public bool IsMoving => false;

    public bool TryMove(LevelObject obj, Vector2 direction) => false;

    public bool CanMove(MovementData data) => false;

    public void Move(MovementData data) {}
}

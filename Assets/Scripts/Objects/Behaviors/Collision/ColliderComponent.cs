using UnityEngine;

public class ColliderComponent : IObjectBehavior
{
    private IColliderBehavior colliderBehavior;

    public void Configure(LevelObjectParameters data)
    {
        colliderBehavior = data.colliderLevel switch
        {
            ColliderLevel.NONE => new NoColliderBehavior(),
            ColliderLevel.LOW => new LowColliderBehavior(),
            ColliderLevel.BASE => new BaseColliderBehavior(),
            ColliderLevel.HIGH => new HighColliderBehavior(),
            _ => new NoColliderBehavior()
        };
    }

    public bool Blocks(MovementType movementType)
    {
        return colliderBehavior != null &&
               colliderBehavior.Blocks(movementType);
    }
}

public enum ColliderLevel
{
    NONE,
    LOW,
    BASE,
    HIGH
}
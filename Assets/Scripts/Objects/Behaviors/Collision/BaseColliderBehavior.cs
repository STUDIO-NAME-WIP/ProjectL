public class BaseColliderBehavior : IColliderBehavior
{
    public bool Blocks(MovementType type)
    {
        return type == MovementType.SWIMMING ||
               type == MovementType.WALKING;
    }
}

public class LowColliderBehavior : IColliderBehavior
{
    public bool Blocks(MovementType type)
    {
        return type == MovementType.SWIMMING;
    }
}

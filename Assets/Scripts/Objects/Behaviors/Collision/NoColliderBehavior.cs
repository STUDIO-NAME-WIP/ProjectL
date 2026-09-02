public class NoColliderBehavior : IColliderBehavior
{
    public bool Blocks(MovementType type)
    {
        return false;
    }
}
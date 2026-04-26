using System.Linq;

public class PickUpper : ObjectHandler<IGrababble>
{
    private bool hasPickup;
    private IGrababble currentPickup;

    public override bool HasPriority => hasPickup;

    public override void Act()
    {
        if (hasPickup)
        {
            currentPickup.Drop();
            hasPickup = false;
        }
        else
        {
            if (objectsNearby.Count <= 0)
                return;
            IGrababble grababble = objectsNearby.OrderBy(i => (i.GetPosition() - transform.position).sqrMagnitude).First();
            grababble.Grab();
            currentPickup = grababble;
            hasPickup = true;
        }
    }
}

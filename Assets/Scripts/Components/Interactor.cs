using System.Linq;

public class Interactor : ObjectHandler<IInteractable>
{
    public override void Act()
    {
        if (objectsNearby.Count <= 0)
            return;
        IInteractable interactable = objectsNearby.OrderBy(i => (i.GetPosition() - transform.position).sqrMagnitude).First();
        interactable.Interact();
    }
}

using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpper : MonoBehaviour
{
    private bool hasPickup;
    private IGrababble currentPickup;

    private List<IGrababble> interactables = new List<IGrababble>();

    public void TryAddGrababble(Collider go)
    {
        if (!go.TryGetComponent<IGrababble>(out var interactable))
            return;

        if (interactables.Contains(interactable))
            return;
        interactables.Add(interactable);
    }

    public void TryRemoveInteractable(Collider go)
    {
        if (!go.TryGetComponent<IGrababble>(out var interactable))
            return;

        if (!interactables.Contains(interactable))
            return;
        interactables.Remove(interactable);
    }

    public void Grab()
    {
        Debug.Log("Grab");
        if (hasPickup)
        {
            currentPickup.Drop();
            hasPickup = false;
        }
        else
        {
            if (interactables.Count <= 0)
                return;
            IGrababble interactable = interactables.OrderBy(i => (i.GetPosition() - transform.position).sqrMagnitude).First();
            interactable.Grab();
            currentPickup = interactable;
            hasPickup = true;
        }

    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactor : MonoBehaviour
{
    [SerializeField] private bool allowInteractionDuringPickup;
    
    private List<IInteractable> interactables = new List<IInteractable>();

    public void TryAddInteractable(Collider go)
    {
        if (!go.TryGetComponent<IInteractable>(out var interactable))
            return;
        
        if (interactables.Contains(interactable))
            return;
        interactables.Add(interactable);
    }
    
    public void TryRemoveInteractable(Collider go)
    {
        if (!go.TryGetComponent<IInteractable>(out var interactable))
            return;
        
        if (!interactables.Contains(interactable))
            return;
        interactables.Remove(interactable);
    }
    
    public void Interact()
    {
        if (interactables.Count <= 0)
            return;
        IInteractable interactable = interactables.OrderBy(i => (i.GetPosition() - transform.position).sqrMagnitude).First();
        interactable.Interact();
    }
}

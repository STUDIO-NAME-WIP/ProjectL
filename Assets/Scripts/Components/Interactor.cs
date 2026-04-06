using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactor : MonoBehaviour
{
    [SerializeField] private bool allowInteractionDuringPickup;
    
    private List<GameObject> interactables = new List<GameObject>();

    public void TryAddInteractable(Collider go)
    {
        //TODO Change to IInteractable once we have it
        if (!go.TryGetComponent<GameObject>(out var interactable))
            return;
        
        if (interactables.Contains(interactable))
            return;
        interactables.Add(interactable);
    }
    
    public void TryRemoveInteractable(Collider go)
    {
        //TODO Change to IInteractable once we have it
        if (!go.TryGetComponent<GameObject>(out var interactable))
            return;
        
        if (!interactables.Contains(interactable))
            return;
        interactables.Remove(interactable);
    }
    
    public void Interact()
    {
        if (interactables.Count <= 0)
            return;
        GameObject interactable = interactables.OrderByDescending(i => i.transform.position.sqrMagnitude).First();
        //TODO Call the interact method on the IInteractable
    }
}

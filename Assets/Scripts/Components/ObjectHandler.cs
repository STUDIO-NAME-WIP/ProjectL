using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectHandler<T> : MonoBehaviour where T : IActuable
{
    protected List<T> objectsNearby = new List<T>();

    public virtual bool HasPriority => false;

    public void TryAddObject(Collider go)
    {
        if (!go.TryGetComponent<T>(out var interactable))
            return;

        if (objectsNearby.Contains(interactable))
            return;
        objectsNearby.Add(interactable);
    }

    public void TryRemoveObject(Collider go)
    {
        if (!go.TryGetComponent<T>(out var interactable))
            return;

        if (!objectsNearby.Contains(interactable))
            return;
        objectsNearby.Remove(interactable);
    }

    public abstract void Act();

    public bool HasObjectsNearby()
    {
        return objectsNearby.Count > 0;
    }

    public Vector2 GetNearestObjectPosition()
    {
        return objectsNearby.OrderBy(i => (i.GetPosition() - transform.position).sqrMagnitude).First().GetPosition();
    }
}
